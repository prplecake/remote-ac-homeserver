package server

import (
	"log"
	"net/http"
	"time"

	"github.com/gorilla/mux"

	"github.com/prplecake/remote-ac-homeserver/app"
	"github.com/prplecake/remote-ac-homeserver/db"
)

// A Server handles routing and dependency injection into the routes.
type Server struct {
	db db.Store
	config app.Configuration
	router *mux.Router
}

// NewServer creates a new Server instnace with the given configuration and database.
func NewServer(config app.Configuration, db db.Store) *Server {
	s := &Server{
		db: db,
		config: config,
		router: mux.NewRouter(),
	}
	s.routes()
	return s
}

func (s *Server) ServeHTTP(w http.ResponseWriter, r *http.Request) {
	start := time.Now()
	defer func() {
		elapsed := time.Since(start)
		log.Printf("Request for url \"%s\" served in %s", r.URL, elapsed)
	}()
	s.router.ServeHTTP(w, r)
}

func (s *Server) routes() {
	r := s.router
	r.HandleFunc("/", s.handleIndex)

	http.Handle("/", r)
	log.Print("Routes initialized")
}
