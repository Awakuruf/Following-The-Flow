# Daoism Light 

An interactive Unity experience where players can engage with a calm, grounded Daoist AI companion inspired by the teachings of Laozi and Zhuangzi.

##  Features

-  **RAG-powered Chatbot**: Integrates a Retrieval-Augmented Generation (RAG) system powered by Mistral 7B Instruct to provide reflective, philosophy-grounded responses.
-  **Immersive Dialogue UI**: Players can interact with NPCs and engage in conversations directly within the game scene.
-  **Dynamic Scenes**: Seamless scene transitions between a peaceful village and a deeper reflection scene.
-  **Daoist Philosophy**: Promotes calm and philosophical reflection with natural-sounding AI dialogue.

## Technologies Used

- **Unity** (2022+)
- **C#** for game logic and UI
- **Python FastAPI** backend for RAG chatbot
- **Mistral-7B-Instruct-v0.2** (with quantization for speed)
- **SentenceTransformers** for semantic search
- **FAISS** for efficient vector similarity
- **Git LFS** for large assets (textures/models)

## Getting Started

### Unity Setup

1. Clone this repo:

   ```bash
   git clone https://github.com/<your-username>/Daoism-Light.git
   cd Daoism-Light
   ```

2. Open in Unity Hub.

3. Ensure scenes are added to your Build Settings.

4. Press Play to explore and interact with the world.
 
- * Make sure your backend is running before pressing Play in Unity. * 

## Folder Structure

```
Assets/
  └── Scripts/
      └── ConversationScene/
      └── Movements/
  └── Scenes/
  └── Textures/ (tracked via Git LFS)
```

## Troubleshooting

- Stuck pushing to GitHub?

Use Git LFS and clean large files with BFG.

## License
MIT License. Feel free to remix or adapt for educational and non-commercial purposes.
