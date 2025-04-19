package main

type UdpMessage struct {
	Hostname    string `json:"hostname"`
	Address     string `json:"address"`
	Port        int    `json:"port"`
	MessageType string `json:"mtype"`
	Message     string `json:"message"`
	Mac         string `json:"mac"`
}

type SensorData struct {
	Temperature float64 `json:"temperature"`
	Humidity    float64 `json:"humidity"`
	Time        string  `json:"timestamp"`
}
