'''
curl https://api.openai-proxy.com/v1/chat/completions \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer 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" \
  -d '{
    "model": "gpt-3.5-turbo",
    "messages": [{"role": "user", "content": "Hello!"}]
  }'

'''


import socket
import sys
import os

from gpt_moreres import ChatGPT

HOST = '127.0.0.1'
PORT = 31415

# os.environ['OPENAI_API_BASE']="https://api.chatanywhere.com.cn/v1/"

chat = ChatGPT("test")

log_file = open("log.txt", "w", encoding="utf-8")

with socket.socket(socket.AF_INET, socket.SOCK_DGRAM) as s:

    s.bind((HOST, PORT))

    print('Listening on', (HOST, PORT))
    while True:
        sys.stdout.flush()
        data, addr = s.recvfrom(1024)
        if(data.decode()):
            print(data.decode(), file=log_file, flush=True)
            chat.messages.append({"role": "user", "content": data.decode()})
            answer = chat.ask_gpt()
            # answer = "你好！测试中文"
            print(answer, file=log_file, flush=True)
            s.sendto(answer.encode(), (HOST, 5768))
            print(f"[ChatGPT]{answer}", flush=True)
