# RabbitChatAvalonia

Simple RabbitMQ sender and consumer test with Avalonia UI.

## Installing RabbitMQ Docker

```bash
docker run -d --hostname rabbit-docker --name rabbit-server -e RABBITMQ_DEFAULT_USER=admin -e RABBITMQ_DEFAULT_PASS=admin -p 5672:5672 -p 15672:15672 rabbitmq:3-management
```