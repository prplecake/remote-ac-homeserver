package server

import (
	"net/http"
	"log"
	"encoding/json"

	"github.com/gorilla/mux"
)

type AppState struct {
	AcUnitOn bool
	WeatherStation string
	WxGridPoints string
}

// @Summary Get application state
// @Description Retrieves the current state of the application
// @Tags Application
// @Accept json
// @Produce json
// @Success 200 {object} AppState
// @Failure 500 {string} string "Internal Server Error"
// @Router /api/state [get]
func (s *Server) getAppState(w http.ResponseWriter, r *http.Request) {
	state := AppState{
		AcUnitOn: true,
		WeatherStation: "KMSP",
		WxGridPoints: "0,0",
	}

	w.Header().Set("Content-Type", "application/json")
	if err := json.NewEncoder(w).Encode(state); err != nil {
		log.Printf("Error encoding state: %v", err)
		http.Error(w, "Internal Server Error", http.StatusInternalServerError)
	}
	log.Printf("App state sent: %+v", state)
}

// NewApiHandler initializes the API routes for the server.
func NewApiHandler(r *mux.Router, s *Server) {
	r.HandleFunc("/state", s.getAppState).Methods(http.MethodGet)

	// Add more API routes here as needed
	log.Print("API routes initialized")
}
