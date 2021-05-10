using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace SMSNhacNo
{
    class ObjectSMSNhacNoDueDate
    {
        private string sysDate;
        private string handPhone;
        private string cardNo;
        private string cardType;
        private string cardBrand;
        private string statementMonth;
        private string closingBalance;
        private string minimumPayment;
        private string dueDate;
        private string maskCard;
        private string totalBalanceIPP;
        private string vip;
        private string cifVip;
        private string sumPaymentCw;

        public ObjectSMSNhacNoDueDate() { }

        public ObjectSMSNhacNoDueDate(string sysDate, string handPhone, string cardNo, string cardType,
                string cardBrand, string statementMonth, string closingBalance, string minimumPayment,
                string dueDate, string maskCard, string totalBalanceIPP, string vip, string cifVip, string sumPaymentCw)
        {
            this.sysDate = sysDate;
            this.handPhone = handPhone;
            this.cardNo = cardNo;
            this.cardType = cardType;
            this.cardBrand = cardBrand;
            this.statementMonth = statementMonth;
            this.closingBalance = closingBalance;
            this.minimumPayment = minimumPayment;
            this.dueDate = dueDate;
            this.maskCard = maskCard;
            this.totalBalanceIPP = totalBalanceIPP;
            this.vip = vip;
            this.cifVip = cifVip;
            this.sumPaymentCw = sumPaymentCw;
        }

        public string getSysDate()
        {
            return this.sysDate;
        }
        public void setSysDate(string sysDate)
        {
            this.sysDate = sysDate;
        }
        public string getHandPhone()
        {
            return this.handPhone;
        }
        public void setHandPhone(string handPhone)
        {
            this.handPhone = handPhone;
        }
        public string getCardNo()
        {
            return this.cardNo;
        }
        public void setCardNo(string cardNo)
        {
            this.cardNo = cardNo;
        }
        public string getCardType()
        {
            return this.cardType;
        }
        public void setCardType(string cardType)
        {
            this.cardType = cardType;
        }
        public string getCardBrand()
        {
            return this.cardBrand;
        }
        public void setCardBrand(string cardBrand)
        {
            this.cardBrand = cardBrand;
        }
        public string getStatementMonth()
        {
            return this.statementMonth;
        }
        public void setStatementMonth(string statementMonth)
        {
            this.statementMonth = statementMonth;
        }
        public string getClosingBalance()
        {
            return this.closingBalance;
        }
        public void setClosingBalance(string closingBalance)
        {
            this.closingBalance = closingBalance;
        }
        public string getMinimumPayment()
        {
            return this.minimumPayment;
        }
        public void setMinimumPayment(string minimumPayment)
        {
            this.minimumPayment = minimumPayment;
        }
        public string getDueDate()
        {
            return this.dueDate;
        }
        public void setDueDate(string dueDate)
        {
            this.dueDate = dueDate;
        }
        public string getMaskCard()
        {
            return this.maskCard;
        }
        public void setMaskCard(string maskCard)
        {
            this.maskCard = maskCard;
        }
        public string getTotalBalanceIPP()
        {
            return this.totalBalanceIPP;
        }
        public void setTotalBalanceIPP(string totalBalanceIPP)
        {
            this.totalBalanceIPP = totalBalanceIPP;
        }
        public string getVip()
        {
            return this.vip;
        }
        public void setVip(string vip)
        {
            this.vip = vip;
        }
        public string getCifVip()
        {
            return this.cifVip;
        }
        public void setCifVip(string cifVip)
        {
            this.cifVip = cifVip;
        }
        public string getSumPaymentCw()
        {
            return this.sumPaymentCw;
        }
        public void setSumPaymentCw(string sumPaymentCw)
        {
            this.sumPaymentCw = sumPaymentCw;
        }
    }
}
