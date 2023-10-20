from gpt import Agent
import json
# 暂时禁用语音
# import speech

def save_to_json(data, filename):
    with open(filename, 'w',encoding="utf-8") as json_file:
        json.dump(data, json_file, indent=4,ensure_ascii=False)  # 使用缩进使 JSON 文件更易读

if __name__ == '__main__':
    # 加载垂直组需要的角色数据
    with open("npc/Evelyn.json", "r", encoding="utf-8") as json_file:
        loaded_data = json.load(json_file)

    # dialog用于存储本次的用户输入和gpt输出，便于加入到下次对话
    new_agent = Agent.from_json(loaded_data)
    dialog = []

    # content实际应该从unity.json中解析到，因为gpt这边是接受content并且只返回本次输出，所以可以在unity侧把上轮的对话加到下一轮的content当中
    content = input("主角：")

    # answer是unity组应该接收到的python.json格式，ask_gpt是单次对话实现
    answer = new_agent.ask_gpt(content)
    print(answer)

    # 可以暂时禁用，因为azure的服务要给钱
    # speech.text_to_speech(answer["content"])

    # 演示pyhton格式
    save_to_json(answer, "python.json")

    # 汇总对话
    dialog.append(f"主角：{content}")
    dialog.append(answer["content"])
    print(dialog)

    # 更新python这边的好感度，用于在对话中控制信息
    save_to_json(new_agent.to_json(),"Evelyn.json")

# 多轮对话:还需要略微改改ask_gpt和change_agent_state的逻辑
# if __name__ == '__main__':
#     # 加载垂直组需要的角色数据
#     with open("Evelyn.json", "r", encoding="utf-8") as json_file:
#         loaded_data = json.load(json_file)
#
#         # dialog用于存储本次的用户输入和gpt输出，便于加入到下次对话
#     new_agent = Agent.from_json(loaded_data)
#     dialog = []
#     next_content = ""
#
#     while True:
#         # content实际应该从unity.json中解析到，因为gpt这边是接受content并且只返回本次输出，所以可以在unity侧把上轮的对话加到下一轮的content当中
#         content = input("主角：")
#         if content == "0":
#             break
#
#         next_content += f"主角：{content}"
#         next_content += "\n"
#
#         # answer是unity组应该接收到的python.json格式，ask_gpt是单次对话实现
#         answer = new_agent.ask_gpt(next_content)
#         print(answer)
#
#         # 可以暂时禁用，因为azure的服务要给钱
#         speech.text_to_speech(answer["content"])
#
#         save_to_json(answer, "python.json")
#
#         dialog.append(f"主角：{content}")
#         dialog.append(answer["content"])
#
#         next_content += answer['content']
#         next_content += "\n"
#
#     print(dialog)
#     # 更新python这边的好感度，用于在对话中控制信息
#     save_to_json(new_agent.to_json(),"Evelyn.json")
