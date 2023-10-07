from gpt import Agent
import json

if __name__ == '__main__':
    with open("abo.json", "r", encoding="utf-8") as json_file:
        loaded_data = json.load(json_file)

    #实例化NPC
    new_agent = Agent.from_json(loaded_data)
    dialog = new_agent.user_chat()
    print(dialog)