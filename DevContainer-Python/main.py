import uvicorn
from transformers import AutoTokenizer, AutoModelForSeq2SeqLM
from fastapi import Request, FastAPI
from fastapi.encoders import jsonable_encoder
from fastapi.responses import JSONResponse
from pydantic import BaseModel

class Body(BaseModel):
    en_text: str

# Load the models and tokenizers
tokenizer = AutoTokenizer.from_pretrained('facebook/blenderbot_small-90M')
model = AutoModelForSeq2SeqLM.from_pretrained('facebook/blenderbot_small-90M')

app = FastAPI()

@app.post("/chat")
async def chat_endpoint(body: Body):
    from_en_text = body.en_text
    inputs = tokenizer(from_en_text, return_tensors="pt")
    reply_ids = model.generate(**inputs)
    output = tokenizer.batch_decode(reply_ids, skip_special_tokens=True)[0]
    return JSONResponse(content=jsonable_encoder({'text': output}))

if __name__ == "__main__":
    uvicorn.run(app, host="0.0.0.0", port=8000)