public class OrderProcessor
{
    private readonly IPaymentGateway _paymentGateway;
    private readonly IInventoryService _inventoryService;
    private readonly INotificationService _notificationService;

    public OrderProcessor(
        IPaymentGateway paymentGateway,
        IInventoryService inventoryService,
        INotificationService notificationService)
    {
        _paymentGateway = paymentGateway;
        _inventoryService = inventoryService;
        _notificationService = notificationService;
    }

    public async Task<OrderResult> ProcessOrder(Order order)
    {
        ValidateOrderNotNull(order);

        if (!IsOrderEligibleForProcessing(order))
        {
            return OrderResult.Invalid("Order validation failed");
        }

        if (!await InventoryIsAvailable(order))
        {
            return OrderResult.Failed("Insufficient inventory");
        }

        await _inventoryService.ReserveItems(order.Items);

        try
        {
            return await CompleteOrderWithPayment(order);
        }
        catch
        {
            await _inventoryService.ReleaseReservation(order.Items);
            throw;
        }
    }

    private static void ValidateOrderNotNull(Order order)
    {
        if (order == null)
        {
            throw new ArgumentNullException(nameof(order));
        }
    }

    private static bool IsOrderEligibleForProcessing(Order order)
    {
        return order.Items?.Count > 0 && order.TotalAmount > 0;
    }

    private async Task<bool> InventoryIsAvailable(Order order)
    {
        return await _inventoryService.CheckAvailability(order.Items);
    }

    private async Task<OrderResult> CompleteOrderWithPayment(Order order)
    {
        var paymentResult = await _paymentGateway.ProcessPayment(
            order.CustomerId,
            order.TotalAmount,
            order.PaymentMethod);

        if (!paymentResult.IsSuccessful)
        {
            await _inventoryService.ReleaseReservation(order.Items);
            return OrderResult.Failed($"Payment failed: {paymentResult.ErrorMessage}");
        }

        await _inventoryService.CommitReservation(order.Items);
        await _notificationService.SendOrderConfirmation(order);

        return OrderResult.Success(paymentResult.TransactionId);
    }

    public async Task CancelOrder(string orderId)
    {
        var order = await GetOrderById(orderId);

        if (order.Status == OrderStatus.Paid)
        {
            await RefundAndRestoreInventory(order);
        }

        order.Status = OrderStatus.Cancelled;
        await SaveOrder(order);
    }

    private async Task RefundAndRestoreInventory(Order order)
    {
        await _paymentGateway.RefundPayment(order.TransactionId);
        await _inventoryService.RestoreInventory(order.Items);
    }

    private async Task<Order> GetOrderById(string orderId)
    {
        return await Task.FromResult(new Order());
    }

    private async Task SaveOrder(Order order)
    {
        await Task.CompletedTask;
    }
}
