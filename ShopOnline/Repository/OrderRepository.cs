using ShopOnline.Data;
using ShopOnline.Models;
using ShopOnline.Models.DBObjects;

namespace ShopOnline.Repository
{
    public class OrderRepository
    {

        private readonly ApplicationDbContext dbContext;

        public OrderRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<OrderModel> GetAllOrders()
        {

            List<OrderModel> orderList = new List<OrderModel>();
            foreach (Order dbcat in this.dbContext.Orders)
            {
                orderList.Add(MapDbObjectToModel(dbcat));
            }
            return orderList;

        }

        public OrderModel GetOrderById(Guid id)
        {
            return MapDbObjectToModel(dbContext.Orders.FirstOrDefault(x => x.IdOrder == id));
        }
        public OrderModel GetOrderByUserId(string userId)
        {
            return MapDbObjectToModel(dbContext.Orders.FirstOrDefault(x => x.IdUser == userId));
        }

        public void InsertOrder(OrderModel orderModel)
        {
            orderModel.IdOrder = Guid.NewGuid();
            dbContext.Orders.Add(MapModelToDbObject(orderModel));
            dbContext.SaveChanges();
        }

        public void UpdateOrder(OrderModel orderModel)
        {
            Order order = dbContext.Orders.FirstOrDefault(x => x.IdOrder == orderModel.IdOrder);
            if (order != null)
            {
                order.IdOrder = orderModel.IdOrder;
                order.IdUser = orderModel.IdUser;
                order.FirstName = orderModel.FirstName;
                order.LastName = orderModel.LastName;
                order.Address = orderModel.Address;
                order.OrderDate = orderModel.OrderDate;
            }
            dbContext.SaveChanges();
        }

        public void DeleteOrder(Guid id)
        {
            var existingOrder = dbContext.Orders.FirstOrDefault(c => c.IdOrder == id);
            if (existingOrder != null)
            {
                dbContext.Orders.Remove(existingOrder);

            }
            dbContext.SaveChanges();
        }

        private OrderModel MapDbObjectToModel(Order dbOrder)
        {
            OrderModel ordermodel = new OrderModel();
            if (dbOrder != null)
            {
                ordermodel.IdOrder = dbOrder.IdOrder;
                ordermodel.IdUser = dbOrder.IdUser;
                ordermodel.FirstName = dbOrder.FirstName;
                ordermodel.LastName = dbOrder.LastName;
                ordermodel.Address = dbOrder.Address;
                ordermodel.OrderDate = dbOrder.OrderDate;
            }
            return ordermodel;
        }

        private Order MapModelToDbObject(OrderModel orderModel)
        {

            Order dbOrder = new Order();
            if (dbOrder != null)
            {
                dbOrder.IdOrder = orderModel.IdOrder;
                dbOrder.IdUser = orderModel.IdUser;
                dbOrder.FirstName = orderModel.FirstName;
                dbOrder.LastName = orderModel.LastName;
                dbOrder.Address = orderModel.Address;
                dbOrder.OrderDate = orderModel.OrderDate;
            }
            return dbOrder;
        }


    }
}
