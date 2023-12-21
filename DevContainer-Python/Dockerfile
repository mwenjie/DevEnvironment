FROM mcr.microsoft.com/devcontainers/python:3.11-bullseye

RUN apt-get update && \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/*

WORKDIR /config

COPY ./config .

# Install Python dependencies from the requirements file
RUN pip install --upgrade --no-cache-dir -r requirements.txt
