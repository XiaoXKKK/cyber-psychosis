from gpt import Agent
import json

def save_to_json(data, filename):
    with open(filename, 'w',encoding="utf-8") as json_file:
        json.dump(data, json_file, indent=4,ensure_ascii=False)  # 使用缩进使 JSON 文件更易读

if __name__ == '__main__':
    # 加载垂直组需要的角色数据
    # 注：本来从设计模式上讲用一个文件存角色数据就好了，但是策划案要求了对话中体现好感度改变，因此也得在这边同步好感度
    with open("Evelyn.json", "r", encoding="utf-8") as json_file:
        loaded_data = json.load(json_file)

    # dialog用于存储本次的用户输入和gpt输出，便于加入到下次对话
    dialog = []
    new_agent = Agent.from_json(loaded_data)

    # content实际应该从unity.json中解析到，因为gpt这边是接受content并且只返回本次输出，所以可以在unity侧把上轮的对话加到下一轮的content当中
    # 可先测通单次对话
    content = input("主角：")

    # answer是unity组应该接收到的python.json格式，ask_gpt是单次对话实现
    answer = new_agent.ask_gpt(content)
    print(answer)
    save_to_json(answer, "python.json")

    dialog.append(f"主角：{content}")
    dialog.append(answer["content"])

    # 更新python这边的好感度，用于在对话中透露信息
    save_to_json(new_agent.to_json(),"Evelyn.json")
