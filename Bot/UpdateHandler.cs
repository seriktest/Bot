using System.Diagnostics;
using System.Text.Json;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Bot;

class UpdateHandler : IUpdateHandler
{
    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        Debug.WriteLine(JsonSerializer.Serialize(update));
        // Вообще, для обработки сообщений лучше подходит паттерн "Цепочка обязанностей", но для примера тут switch-case
        // https://refactoring.guru/ru/design-patterns/chain-of-responsibility
        switch (update)
        {
            case
            {
                Type: UpdateType.Message,
                Message: { Text: { } text, Chat: { } chat },
            } when text.Equals("/start", StringComparison.OrdinalIgnoreCase):
            {
                await botClient.SendTextMessageAsync(chat!, "Добро пожаловать на борт, добрый путник!", cancellationToken: cancellationToken);
                break;
            }
            case
            {
                Type: UpdateType.Message,
                Message.Chat: { } chat
            }:
            {
                await botClient.SendTextMessageAsync(chat!, "Привет-привет!!", cancellationToken: cancellationToken);
                break;
            }
        }
    }
    public Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        Console.Error.WriteLine(exception);
        return Task.CompletedTask;
    }
}

