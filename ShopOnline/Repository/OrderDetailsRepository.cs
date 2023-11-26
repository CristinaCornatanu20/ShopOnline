
using ShopOnline.Data;
using ShopOnline.Models.DBObjects;
using ShopOnline.Models;

namespace ShopOnline.Repository
{
    public class OrderDetailsRepository
    {

        private readonly ApplicationDbContext dbContext;

        public OrderDetailsRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<OrderDetailsModel> GetAllOrderDetails()
        {

            List<OrderDetailsModel> orderList = new List<OrderDetailsModel>();
            foreach (OrderDetail dbcat in this.dbContext.OrderDetails)
            {
                orderList.Add(MapDbObjectToModel(dbcat));
            }
            return orderList;

        }

        public OrderDetailsModel GetOrderDetailsById(Guid id)
        {
            return MapDbObjectToModel(dbContext.OrderDetails.FirstOrDefault(x => x.IdOrderDetails == id));
        }
        public List<OrderDetailsModel> GetOrderDetailsByOrderId(Guid orderId)
        {
            var orderDetailsList = new List<OrderDetailsModel>();
            var orderDetails = dbContext.OrderDetails.Where(od => od.IdOrder == orderId);

            foreach (var dbOrderDetails in orderDetails)
            {
                orderDetailsList.Add(MapDbObjectToModel(dbOrderDetails));
            }

            return orderDetailsList;
        }
        public List<OrderDetailsModel> GetOrderDetailsByUserId(string userId)
        {
            // Obține toate detaliile comenzii pentru un anumit utilizator
            return dbContext.OrderDetails
                .Where(o => o.IdOrderNavigation.IdUser == userId)
                .Select(MapDbObjectToModel) // Mapează detaliile comenzii la model
                .ToList();
        }
        public void InsertOrderDetails(OrderDetailsModel orderModel)
        {
            orderModel.IdOrder = Guid.NewGuid();
            dbContext.OrderDetails.Add(MapModelToDbObject(orderModel));
            dbContext.SaveChanges();
        }

        public void UpdateOrderDetails(OrderDetailsModel orderModel)
        {
            OrderDetail order = dbContext.OrderDetails.FirstOrDefault(x => x.IdOrderDetails == orderModel.IdOrderDetails);
            if (order != null)
            {
                order.IdOrderDetails = orderModel.IdOrderDetails;
                order.IdOrder = orderModel.IdOrder;
                order.IdProduct = orderModel.IdProduct;
                order.IdTva = orderModel.IdTva;
                order.PriceTva = orderModel.PriceTva;
                order.Quantity = orderModel.Quantity;
                order.PriceTotal = orderModel.PriceTotal;
            }
            dbContext.SaveChanges();
        }

        public void DeleteOrderDetails(Guid id)
        {
            var existingOrder = dbContext.OrderDetails.FirstOrDefault(c => c.IdOrderDetails == id);
            if (existingOrder != null)
            {
                dbContext.OrderDetails.Remove(existingOrder);

            }
            dbContext.SaveChanges();
        }

        private OrderDetailsModel MapDbObjectToModel(OrderDetail dbOrder)
        {
            OrderDetailsModel ordermodel = new OrderDetailsModel();
            if (dbOrder != null)
            {
                
                ordermodel.IdOrderDetails = dbOrder.IdOrderDetails;
                ordermodel.IdOrder = dbOrder.IdOrder;
                ordermodel.IdProduct = dbOrder.IdProduct;
                ordermodel.IdTva = dbOrder.IdTva;
                ordermodel.PriceTva = dbOrder.PriceTva;
                ordermodel.Quantity = dbOrder.Quantity;
                ordermodel.PriceTotal = dbOrder.PriceTotal;
            }
            return ordermodel;
        }

        private OrderDetail MapModelToDbObject(OrderDetailsModel orderModel)
        {

            OrderDetail dbOrder = new OrderDetail();
            if (dbOrder != null)
            {
                dbOrder.IdOrderDetails = orderModel.IdOrderDetails;
                dbOrder.IdOrder = orderModel.IdOrder;
                dbOrder.IdProduct = orderModel.IdProduct;
                dbOrder.IdTva = orderModel.IdTva;
                dbOrder.PriceTva = orderModel.PriceTva;
                dbOrder.Quantity = orderModel.Quantity;
                dbOrder.PriceTotal = orderModel.PriceTotal;
            }
            return dbOrder;
        }

    }
}
