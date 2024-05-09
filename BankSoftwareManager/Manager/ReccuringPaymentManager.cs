using BankSoftwareDataAccess.IRepository;
using BankSoftwareDataAccess.Repository;
using BankSoftwareManager.IManager;
using BankSoftwareModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSoftwareManager.Manager
{
    public class ReccuringPaymentManager : IReccuringPaymentManager
    {
        private readonly IRecurringPaymentRepository _recurringPaymentRepository;
        private readonly IPaymentFrequencyRepository _paymentFrequencyRepository;
        private readonly IPaymentDetailRepository _paymentDetailRepository;
        private readonly IAccountRepository _accountRepository;
        public ReccuringPaymentManager(IAccountRepository accountRepository,IRecurringPaymentRepository recurringPaymentRepository, IPaymentFrequencyRepository paymentFrequencyRepository, IPaymentDetailRepository paymentDetailRepository)
        {
            _recurringPaymentRepository = recurringPaymentRepository;
            _paymentFrequencyRepository = paymentFrequencyRepository;
            _paymentDetailRepository = paymentDetailRepository;
            _accountRepository = accountRepository;
        }

        public Guid SaveUpdateRecurringPayment(RecurringPayment model)
        {
            return _recurringPaymentRepository.SaveUpdateRecurringPayment(model);
        }
        public List<RecurringPayment> GetAllRecurringPayment()
        {
            var payments =_recurringPaymentRepository.GetAllRecurringPayment();
            foreach (var item in payments)
            {
                item.CustomerAccountName = _accountRepository.GetAccountById(item.CustomerAccountFk).AccountName;
                item.RecievingAccountName = _accountRepository.GetAccountById(item.ReceivableAccountFk).AccountName;
                item.FrequencyString = _paymentFrequencyRepository.GetPaymentFrequencyById(item.FrequencyFk).Name;
                item.StartDateString = item.StartDate.ToString();
            }
            return payments;
        }
        public RecurringPayment GetRecurringPaymentById(Guid accountId)
        {
            return _recurringPaymentRepository.GetRecurringPaymentById(accountId);
        }

        ///Frequency
        ///
        public Guid SaveUpdatePaymentFrequency(PaymentFrequency model)
        {
            return _paymentFrequencyRepository.SaveUpdatePaymentFrequency(model);
        }
        public List<PaymentFrequency> GetAllPaymentFrequency()
        {
            return _paymentFrequencyRepository.GetAllPaymentFrequency();
        }
        public PaymentFrequency GetPaymentFrequencyById(Guid frequencyId)
        {
            return _paymentFrequencyRepository.GetPaymentFrequencyById(frequencyId);
        }


        ///Payment Detail
        public Guid SaveUpdatePaymentDetail(PaymentDetail model)
        {
            return _paymentDetailRepository.SaveUpdatePaymentDetail(model);
        }
        public List<PaymentDetail> GetAllPaymentDetail()
        {
            return _paymentDetailRepository.GetAllPaymentDetail();
        }
        public List<PaymentDetail> GetAllPaymentDetailByRecId(Guid recPaymentId)
        {
            return _paymentDetailRepository.GetAllPaymentDetailByRecId(recPaymentId);
        }
        public PaymentDetail GetPaymentDetailById(Guid frequencyId)
        {
            return _paymentDetailRepository.GetPaymentDetailById(frequencyId);
        }
        public Guid SaveUpdatePaymentDetailList(List<PaymentDetail> model)
        {
            return _paymentDetailRepository.SaveUpdatePaymentDetailList(model);
        }

         
    }
}
