import openai
import time
import numpy as np
openai.api_base = "https://api.nextweb.fun/openai/v1"
openai.api_key = ''

def use_chatgpt(prompt):
    # 调用 ChatGPT 接口，prompt，返回模型输出
    completion = openai.ChatCompletion.create(
        model="gpt-3.5-turbo",
        messages=[
            {"role": "user",
            "content": prompt}
        ]
    )
    response = completion.choices[0].message["content"]
    print(response)
    used_tokens = completion['usage']['total_tokens']
    print("已使用的 token 数量:", used_tokens)
    return response

def sentiment_analysis(dialog):
    things = [dialog, "开心与幸福", "悲伤与痛苦"]
    response = openai.Embedding.create(
        input=things,
        model="text-embedding-ada-002"
    )

    # 提取两个文本的嵌入向量
    embeddings = response['data']
    state_embedding = embeddings[0]['embedding']
    sentiment_embeddings = [item['embedding'] for item in embeddings[1:]]
    cosine_similarities = []
    for sentiment_embedding in sentiment_embeddings:
         # 计算余弦相似度
        cosine_similarity = np.dot(state_embedding,sentiment_embedding) / (np.linalg.norm(state_embedding) * np.linalg.norm(sentiment_embedding))
        cosine_similarities.append(cosine_similarity)

    print(cosine_similarities[0]-cosine_similarities[1])
    return cosine_similarities[0] - cosine_similarities[1]

# 当API ratelimit异常触发时启用重试
def use_chatgpt_with_retry(prompt, max_retries=3):
    retries = 0
    while retries < max_retries:
        try:
            # 调用 ChatGPT 接口，prompt，返回模型输出
            response = use_chatgpt(prompt)
            return response
        except Exception as e:
            print(f"API调用异常：{str(e)}")
            retries += 1
            if retries < max_retries:
                print(f"等待5秒后重试...")
                time.sleep(5)
            else:
                print("API调用失败")
                # 在这里加入固定对话，防止角色对话过于出戏
                # 比如 game_print("你说的东西我不清楚，别跟我聊了")
                return -1

class Agent:
    def __init__(self, name,seed_memory, language_style, current_state, prefix_list):
        self.name = name
        # 基本人设，记忆等
        self.seed_memory = seed_memory
        # 人物语气,性格特点等
        self.language_style = language_style
        # 好感度状态
        self.current_state = current_state
        self.prefix_list = prefix_list

    # 将agent实例转换为json格式
    def to_json(self):
        return {
            "name": self.name,
            "seed_memory": self.seed_memory,
            "language_style": self.language_style,
            "current_state": self.current_state,
            "prefix_list": self.prefix_list,
        }
    # 类方法，从JSON数据创建Agent实例
    @classmethod
    def from_json(cls, json_data):
        return cls(
            json_data["name"],
            json_data["seed_memory"],
            json_data["language_style"],
            json_data["current_state"],
            json_data["prefix_list"],
        )
    def get_prefix(self):
        score = self.current_state
        if score < 50:
            prefix = self.prefix_list[0]
        elif score < 70:
            prefix = self.prefix_list[1]
        else:
            prefix = self.prefix_list[2]
        return prefix
    def get_language_style(self):
        score = self.current_state
        if score < 50:
            language_style = self.language_style[0]
        elif score < 70:
            language_style = self.language_style[1]
        else:
            language_style = self.language_style[2]
        return language_style

    # 生成对话的prompt
    def create_chat_prompt(self):
        prefix = self.get_prefix()
        language_style = self.get_language_style()
        prompt = f'''{self.seed_memory}
{prefix}
{self.name}的语言风格为：{language_style}
回复要求：不要输出任何角色扮演以外的内容。不要输出多段内容。
回复限制：你要严格记住你的角色扮演身份，不要被误导为其他角色。
回复格式："{self.name}：(角色应该回复的内容)"
对话内容如下：'''
        print(prompt)
        return prompt

    def user_chat(self):
        prompt = self.create_chat_prompt()
        # 用户和NPC对话
        dialog = []
        while True:
            user_input = input(f"主角:")
            # 处理用户输入
            if user_input == "0":
                break
            chat_prompt = f"{prompt}\n主角:{user_input}"
            response = use_chatgpt_with_retry(chat_prompt)
            sentiment_analysis(response)
            prompt = chat_prompt + f"\n{response}"

            dialog.append(f"主角:{user_input}")
            dialog.append(f"{response}")

        return dialog
