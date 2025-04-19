package main

import (
	"encoding/json"
	"fmt"
	"net"
)

func main() {
	local, err := net.ResolveUDPAddr("udp", ":42069")
	if err != nil {
		panic(err)
	}
	remote, err := net.ResolveUDPAddr("udp", "10.0.13.255:9876")
	if err != nil {
		panic(err)
	}
	list, err := net.DialUDP("udp", local, remote)
	if err != nil {
		panic(err)
	}
	defer func(list *net.UDPConn) {
		err := list.Close()
		if err != nil {
			panic(err)
		}
	}(list)

	ip, err := getLocalIP()
	if err != nil {
		panic(err)
	}

	mac, err := getMacAddrForIp(ip)
	if err != nil {
		panic(err)
	}
	fmt.Println("Local IP:", ip)
	fmt.Println("Local MAC:", mac)

	message := UdpMessage{
		Hostname:    "sensor1",
		Address:     ip.String(),
		Port:        list.LocalAddr().(*net.UDPAddr).Port,
		Mac:         mac,
		MessageType: "REGISTRATION",
		Message:     "",
	}

	messageJson, err := json.Marshal(message)
	if err != nil {
		panic(err)
	}

	_, err = list.Write(messageJson)
	if err != nil {
		panic(err)
	}
}
