using Bot;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;

var botClient = new TelegramBotClient("");

var cts = new CancellationTokenSource();
Console.CancelKeyPress += (_, _) => cts.Cancel(); // Чтобы отловить нажатие ctrl+C 

var handler = new UpdateHandler();
var receiverOptions = new ReceiverOptions();

botClient.StartReceiving(handler, receiverOptions, cancellationToken: cts.Token);

Console.WriteLine("Bot started. Press ^C to stop");
await Task.Delay(-1, cancellationToken: cts.Token);
Console.WriteLine("Bot stopped");
Console.WriteLine("Hello, World!");
