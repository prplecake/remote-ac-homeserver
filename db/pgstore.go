package db

import (
	"database/sql"
	"fmt"

	_ "github.com/lib/pq"
	"github.com/qustavo/dotsql"

	"github.com/prplecake/remote-ac-homeserver/app"
)

// A PGStore implements storage against a PostgreSQL database.
type PGStore struct {
	conn *sql.DB
}

func NewPGStore(dbConfig app.DatabaseConfig) (*PGStore, error) {
	dbinfo := fmt.Sprintf("user=%s password=%s dbname=%s sslmode=disable",
		dbConfig.Username,
		dbConfig.Password,
		dbConfig.Name)
	db, err := sql.Open("postgres", dbinfo)
	if err != nil {
		return nil, err
	}
	db.SetMaxIdleConns(5)
	return &PGStore{conn: db}, nil
}

// ExecuteSchema runs all SQL commands in the given file to initialize the database.
func (pg *PGStore) ExecuteSchema(file string) error {
	dot, err := dotsql.LoadFromFile(file)
	if err != nil {
		return err
	}
	for query := range dot.QueryMap() {
		_, err := dot.Exec(pg.conn, query)
		if err != nil {
			return err
		}
	}
	return nil
}


