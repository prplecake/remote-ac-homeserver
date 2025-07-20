package app

import (
	"html/template"
	"log"
	"net/http"
	"time"
)

var (
	templates *template.Template
	CurrentVersion string
	CurrentBranch string
)

// InitTemplates initializes the template engine with the given configuration.
func InitTemplates(path string) {
	templates = template.Must(template.ParseGlob(path))
	log.Println("Initialized templates from", path)
}

func RenderTemplate(w http.ResponseWriter, template string, data interface{}) {
	if templates == nil {
		panic("Templates uninitialized")
	}

	start := time.Now()
	defer func() {
		elapsed := time.Since(start)
		log.Printf("Rendered template \"%s\" in %s", template, elapsed)
	}()

	err := templates.ExecuteTemplate(w, template+".html", data)
	if err != nil {
		log.Println(err)
		//ThrowInternalServerError(w)
	}
}
