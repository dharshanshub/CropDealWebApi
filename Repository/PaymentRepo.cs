using CropDealWebAPI.Models;

namespace CropDealWebAPI.Repository
{
    public class PaymentRepo : IPaymentRepository
    {
        CropDealContext _context;
        public PaymentRepo(CropDealContext context) => _context = context;

        #region Payment
        /// <summary>
        /// this method is exwcuted after payment to gerate invoice and to add invoice
        /// </summary>
        /// <param name="payment"></param>
        /// <returns></returns>
        public string AddPayment(Payment payment)
        {
            try
            {
                var Crpid = payment.CropAdid;

                var query =
                    from f in _context.CropOnSales
                    where Crpid == f.CropAdId
                    join g in _context.UserProfiles
                         on payment.FarmerId equals g.UserId
                    join d in _context.UserProfiles
                         on payment.UserId equals d.UserId
                    select new Invoice()
                    {
                        CropName = f.CropName,
                        CropQty = f.CropQty,
                        CropType = f.CropType,
                        OrderTotal = f.CropPrice,
                        InvoiceDate = DateTime.Now,
                        FarmerAccNumber = g.UserAccnumber,
                        DealerAccNumber = d.UserAccnumber

                    };
                List<Invoice> invoices = query.ToList();
                Invoice invoice1 = new Invoice();
                foreach (var invoice in invoices)
                {
                    invoice1.CropName = invoice.CropName;
                    invoice1.CropQty = invoice.CropQty;
                    invoice1.CropType = invoice.CropType;
                    invoice1.OrderTotal = invoice.OrderTotal;
                    invoice1.InvoiceDate = invoice.InvoiceDate;
                    invoice1.FarmerAccNumber = invoice.FarmerAccNumber;
                    invoice1.DealerAccNumber = invoice.DealerAccNumber;

                }

                if (AddInvoice(invoice1))
                {
                    return "200";
                }
                else
                {
                    return "400";
                }
            }catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }

        }
        public bool AddInvoice(Invoice invoice)
        {
            try
            {
                _context.Invoices.Add(invoice);
                _context.SaveChangesAsync();
                var response = true;
                return response;
            }catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }
        }
        #endregion

        #region FarmersInvoice
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public List<Invoice> ViewInvoiceAsync(int UserId)
        {
            try
            {
                var query = from a in _context.UserProfiles
                            where a.UserId == UserId
                            select new Invoice()
                            {
                                FarmerAccNumber = a.UserAccnumber
                            };

                List<Invoice> invoice = query.ToList();
                int Accno;
                foreach (var i in invoice)
                {
                    Accno = i.FarmerAccNumber;



                    var res = from b in _context.Invoices
                              where Accno == b.FarmerAccNumber
                              select new Invoice()
                              {
                                  InvoiceId = b.InvoiceId,
                                  CropName = b.CropName,
                                  CropQty = b.CropQty,
                                  CropType = b.CropType,
                                  OrderTotal = b.OrderTotal,
                                  InvoiceDate = b.InvoiceDate,
                                  FarmerAccNumber = b.FarmerAccNumber


                              };


                    List<Invoice> invoices = res.ToList();
                    return invoices;
                }
                return null;
            }catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }



        }
        #endregion

        #region DealerInvoice
        /// <summary>
        /// this method is used get dealer invoice
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public List<Invoice> ViewDealerInvoiceAsync(int UserId)
        {
            try
            {
                var query = from a in _context.UserProfiles
                            where a.UserId == UserId
                            select new Invoice()
                            {
                                DealerAccNumber = a.UserAccnumber
                            };

                List<Invoice> invoice = query.ToList();
                int Accno;
                foreach (var i in invoice)
                {
                    Accno = i.DealerAccNumber;



                    var res = from b in _context.Invoices
                              where Accno == b.DealerAccNumber
                              select new Invoice()
                              {
                                  InvoiceId = b.InvoiceId,
                                  CropName = b.CropName,
                                  CropQty = b.CropQty,
                                  CropType = b.CropType,
                                  OrderTotal = b.OrderTotal,
                                  InvoiceDate = b.InvoiceDate,
                                  DealerAccNumber = b.DealerAccNumber


                              };


                    List<Invoice> invoices = res.ToList();
                    return invoices;
                }
                return null;
            }
            catch
            {
                throw;
            }
            finally { }

        }
        #endregion
    }
}
