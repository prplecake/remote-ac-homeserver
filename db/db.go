package db

import (
	"database/sql"
	"errors"
	"log"
)

var (
	ErrNotFound = errors.New("DB: not found")
)

func (pg *PGStore) Query(query string, args ...interface{}) (*sql.Rows, error) {
	log.Printf("Running query: %s with args: %v", query, args)
	return pg.conn.Query(query, args...)
}

// A Store provdes methods required to interact with the database.
type Store interface {

}
