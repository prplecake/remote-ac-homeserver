package server

import (
	"log"
	"net/http"
	"time"

	"github.com/gorilla/mux"
	"github.com/swaggo/http-swagger/v2"

	_ "github.com/prplecake/remote-ac-homeserver/docs"
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

	NewApiHandler(r.PathPrefix("/api").Subrouter(), s)

	r.PathPrefix("/swagger").Handler(httpSwagger.Handler(
		httpSwagger.URL("http://localhost:" + s.config.App.Port + "/swagger/doc.json"),
	)).Methods(http.MethodGet)
	http.Handle("/", r)
	log.Print("Routes initialized")
}
