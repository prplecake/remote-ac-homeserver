package server

import (
	"net/http"

	"github.com/prplecake/remote-ac-homeserver/app"
)

func (s *Server) handleIndex(w http.ResponseWriter, r *http.Request) {
	app.RenderTemplate(w, "index", nil)
}
