public class ChatHub : Hub
{
    // Stores userId -> connectionId
    private static readonly Dictionary<string, string> _connections = new();

    public override Task OnConnectedAsync()
    {
        var userId = Context.GetHttpContext()?.Request.Query["userId"].ToString();

        if (!string.IsNullOrWhiteSpace(userId))
        {
            _connections[userId] = Context.ConnectionId;
        }

        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = _connections.FirstOrDefault(x => x.Value == Context.ConnectionId).Key;
        if (userId != null)
        {
            _connections.Remove(userId);
        }

        return base.OnDisconnectedAsync(exception);
    }

    public async Task SendPrivateMessage(string senderId, string receiverId, string message)
    {
        // Send to receiver if online
        if (_connections.TryGetValue(receiverId, out string receiverConnId))
        {
            await Clients.Client(receiverConnId).SendAsync("ReceiveMessage", senderId, message);
        }

        // Send confirmation to sender
        if (_connections.TryGetValue(senderId, out string senderConnId))
        {
            await Clients.Client(senderConnId).SendAsync("MessageSent", receiverId, message);
        }

        // Optionally: Store message in DB
    }
}
