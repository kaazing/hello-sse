#!/usr/bin/env python

import requests                             # pip install requests
from hyper.contrib import HTTP20Adapter     # pip install hyper
import sseclient                            # pip install sseclient-py
import sys                                  # ...only used to grab sys.argv                           

def main():
    
    if (len(sys.argv) != 2):
        print("Usage: sse.py <Server Sent Events URL>")
        exit()

    session = requests.Session()
    session.mount('https://', HTTP20Adapter())
    response = session.get(sys.argv[1], stream=True)
    client = sseclient.SSEClient(response)

    try:
        while True:
            try:
                event = next(client.events())
                # SSE event ID contained in event.id
                print(event.data)
            except Exception:
                continue
    except KeyboardInterrupt:
        print("Exiting...")

if __name__ == "__main__":
    # Execute only if run as script
    main()
