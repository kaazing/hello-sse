package main

import (
	"fmt"
	"os"

	"github.com/donovanhide/eventsource"
)

func main() {

	var url string

	if len(os.Args) > 1 {
		url = os.Args[1]
	} else {
		fmt.Println("Usage: go sse.go <Server Sent Events URL>")
		os.Exit(1)
	}

	stream, err := eventsource.Subscribe(url, "")
	if err != nil {
		return
	}
	for true {
		ev := <-stream.Events
		// Event Source members: ev.Id(), ev.Event(), ev.Data()
		fmt.Println(ev.Data())
	}

}
