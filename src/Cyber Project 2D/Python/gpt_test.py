import socket
import os
import json

from gpt import Agent

HOST = '127.0.0.1'
PORT = 31415

def save_to_json(data, filename):
    with open(filename, 'w',encoding="utf-8") as json_file:
        json.dump(data, json_file, indent=4,ensure_ascii=False)  # 使用缩进使 JSON 文件更易读

import glob

agent_dict = {}

for file in glob.glob("./npc/*.json"):
    with open(file, "r", encoding="utf-8") as json_file:
        loaded_data = json.load(json_file)
        agent_name = os.path.splitext(os.path.basename(file))[0]
        agent_dict[agent_name] = Agent.from_json(loaded_data)

with socket.socket(socket.AF_INET, socket.SOCK_DGRAM) as s:

    s.bind((HOST, PORT))

    print('Listening on', (HOST, PORT))
    while True:
        # sys.stdout.flush()
        data, addr = s.recvfrom(1024)
        if(data.decode()):
            json_data = json.loads(data.decode())
            # 根据json_data["npc"]找到对应的agent
            agent = agent_dict[json_data["name"]]
            answer = agent.ask_gpt(json_data["content"], json_data["now_state"])
            s.sendto(json.dumps(answer, ensure_ascii=False).encode(), (HOST, 5768))
            # save_to_json(Evelyn_Agent.to_json(),"Evelyn.json")
