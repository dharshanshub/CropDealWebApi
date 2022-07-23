﻿using CropDealWebAPI.Models;
using CropDealWebAPI.Repository;

namespace CropDealWebAPI.Service
{
    public class PaymentService
    {
        private IPaymentRepository _repository;
        public PaymentService(IPaymentRepository repository)
        {
            _repository = repository;
        }
        public string AddPayment(Payment payment)
        {
            return _repository.AddPayment(payment);
        }
    }
}