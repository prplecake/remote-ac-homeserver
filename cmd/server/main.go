package main

import (
	"log"
	"net/http"
	"os"

	"github.com/prplecake/remote-ac-homeserver/app"
	"github.com/prplecake/remote-ac-homeserver/db"
	"github.com/prplecake/remote-ac-homeserver/server"
	"gopkg.in/yaml.v2"
)

var (
	config app.Configuration
	store *db.PGStore
)

func main() {
	log.Print("Initializing remote-ac-homeserver...")
	loadConfig()

	if len(os.Args) > 1 {
		processArgs()
	} else {
		runServer()
	}
}

func loadConfig() {
	configFile := "cmd/server/config.yaml"
	cf, err := os.ReadFile(configFile)
	if err != nil {
		log.Panic(err)
	}
	err = yaml.Unmarshal(cf, &config)
	if err != nil {
		log.Panic(err)
	}
}

func processArgs() {}

func runServer() {
	app.InitTemplates(config.Templates.Path + "/**")

	postgres, err := db.NewPGStore(config.Database)
	if err != nil {
		log.Fatal(err)
	}

	log.Fatal(http.ListenAndServe(
		":"+config.App.Port,
		server.NewServer(config, postgres)))
	}
