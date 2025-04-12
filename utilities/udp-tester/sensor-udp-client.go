package main

import (
	"encoding/json"
	"fmt"
	"log"
	"math/rand"
	"net"
	"os"
	"time"
)

type UdpMessage struct {
	Hostname    string `json:"hostname"`
	Address     string `json:"address"`
	Port        int    `json:"port"`
	MessageType string `json:"mtype"`
	Message     string `json:"message"`
}

type SensorData struct {
	Temperature float64 `json:"temperature"`
	Humidity    float64 `json:"humidity"`
	Time        string  `json:"timestamp"`
}

func convert_fahrenheit_to_celcius(f float64) float64 {
	return (f - 32) * 5 / 9
}

func main() {
	// Create a UDP address for the server
	serverAddr, err := net.ResolveUDPAddr("udp", "localhost:9876")
	if err != nil {
		fmt.Println("Error resolving server address:", err)
		os.Exit(1)
	}

	// Create a UDP connection
	conn, err := net.DialUDP("udp", nil, serverAddr)
	if err != nil {
		fmt.Println("Error creating UDP connection:", err)
		os.Exit(1)
	}
	defer conn.Close()
	log.Printf("Connected to server %s\n", conn.RemoteAddr())
	log.Printf("Local address: %s\n", conn.LocalAddr())

	// Send sensor data every ten seconds
	for {
		// Random number from 60 to 120
		temp := rand.Intn(60) + 60
		// Random number from 0 to 100
		humidity := rand.Intn(100)
		// Sensor data JSON payload
		var sensorData = SensorData{
			Temperature: float64(convert_fahrenheit_to_celcius(float64(temp))),
			Humidity:    float64(humidity),
		}
		// Get the current time
		currentTime := time.Now().Format(time.RFC3339)
		sensorData.Time = currentTime
		// Convert sensor data to JSON
		sensorDataJson, err := json.Marshal(sensorData)
		if err != nil {
			fmt.Println("Error marshalling sensor data:", err)
			break
		}
		// Create a message
		message := UdpMessage{
			Hostname:    "sensor1",
			Address:     "localhost",
			Port:        conn.LocalAddr().(*net.UDPAddr).Port,
			MessageType: "SENSOR_DATA",
			Message:     string(sensorDataJson),
		}
		// Convert message to JSON
		messageJson, err := json.Marshal(message)
		if err != nil {
			fmt.Println("Error marshalling message:", err)
			break
		}

		_, err = conn.Write([]byte(messageJson))
		if err != nil {
			fmt.Println("Error sending data:", err)
			break
		}
		fmt.Println("Sent:", sensorData)
		time.Sleep(10 * time.Second)
	}
}
