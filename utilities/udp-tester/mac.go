package main

import (
	"fmt"
	"net"
)

func getMacAddrForIp(ip net.IP) (string, error) {
	// Get the network interface for the given IP address
	iface, err := getInterfaceByIp(ip)
	if err != nil {
		return "", err
	}
	// Get the hardware address (MAC address) of the interface
	mac := iface.HardwareAddr

	// Convert the MAC address to a string format
	macStr := mac.String()

	return macStr, nil
}
func getInterfaceByIp(ip net.IP) (*net.Interface, error) {
	ifaces, err := net.Interfaces()

	if err != nil {
		return nil, err
	}

	for _, iface := range ifaces {
		addrs, err := iface.Addrs()
		if err != nil {
			return nil, err
		}
		for _, addr := range addrs {
			ipNet, ok := addr.(*net.IPNet)
			if !ok {
				continue
			}
			if ipNet.IP.Equal(ip) {
				return &iface, nil
			}
		}
	}
	return nil, fmt.Errorf("no interface found")
}
