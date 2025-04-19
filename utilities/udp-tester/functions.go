package main

import (
	"fmt"
	"net"
)

func getLocalIP() (net.IP, error) {
	interfaces, err := net.Interfaces()
	if err != nil {
		return nil, err
	}

	for _, iface := range interfaces {
		// Skip interfaces that are down or loopback
		if iface.Flags&net.FlagUp == 0 || iface.Flags&net.FlagLoopback != 0 {
			continue
		}

		addrs, err := iface.Addrs()
		if err != nil {
			return nil, err
		}

		for _, addr := range addrs {
			var ip net.IP

			// Check if the address is an IP address
			switch v := addr.(type) {
			case *net.IPNet:
				ip = v.IP
			case *net.IPAddr:
				ip = v.IP
			}

			// Skip loopback and non-IPv4 addresses
			if ip == nil || ip.IsLoopback() || ip.To4() == nil {
				continue
			}

			return ip, nil
		}
	}

	return nil, fmt.Errorf("no valid IP address found")
}
