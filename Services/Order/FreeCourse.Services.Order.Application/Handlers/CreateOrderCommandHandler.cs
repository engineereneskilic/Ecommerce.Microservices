using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeCourse.Services.Order.Application.Commands;
using FreeCourse.Services.Order.Application.Dtos;
using FreeCourse.Services.Order.Domain.OrderAggregate;
using FreeCourse.Services.Order.Infrastructure;

using FreeCourse.Shared.Dtos;
using MediatR;

namespace FreeCourse.Services.Order.Application.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderComment, ResponseDto<CreatedOrderDto>>
    {
        private readonly OrderDbContext _orderDbContext;

        public CreateOrderCommandHandler(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public async Task<ResponseDto<CreatedOrderDto>> Handle(CreateOrderComment request, CancellationToken cancellationToken)
        {
            var newAddress = new Address(request.addressDto.Province, request.addressDto.District, request.addressDto.Street, request.addressDto.ZipCode, request.addressDto.Line);

            Domain.OrderAggregate.Order newOrder = new Domain.OrderAggregate.Order(request.BuyerId, newAddress);

            request.OrderItems.ForEach(x =>
            {
                newOrder.AddOrderItem(x.ProductID, x.ProductName, x.Price, x.PictureUrl);
            });

            await _orderDbContext.Orders.AddAsync(newOrder);
            var result = await _orderDbContext.SaveChangesAsync();

            //return ResponseDto<CreatedOrderDto>.Success(new CreatedOrderDto { OrderedId = newOrder.Id },200);
            return ResponseDto<CreatedOrderDto>.Success(new CreatedOrderDto { OrderedId = newOrder.Id }, 200);
        }

    }
}