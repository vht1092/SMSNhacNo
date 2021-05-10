using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMSNhacNo
{
    class ObjectSMSNhacNoSaoKe
    {
        private string sdate;
        private string handPhone;
        private string cardNo;
        private string cardType;
        private string cardBrn;
        private string stateMonth;
        private string closingBalance;
        private string minimumPayment;
        private string dueDate;
        private string cardMask;
        private string totBalIPP;
        private string vip;
        private string cifVip;
        private string idStatement;

        public ObjectSMSNhacNoSaoKe() { }

        public ObjectSMSNhacNoSaoKe(string sdate, string handPhone, string cardNo
            , string cardType, string cardBrn, string stateMonth
            , string closingBalance, string minimumPayment, string dueDate
            , string cardMask, string totBalIPP, string vip
            , string cifVip, string idStatement)
        {
            this.sdate = sdate;
            this.handPhone = handPhone;
            this.cardNo = cardNo;

            this.cardType = cardType;
            this.cardBrn = cardBrn;
            this.stateMonth = stateMonth;

            this.closingBalance = closingBalance;
            this.minimumPayment = minimumPayment;
            this.dueDate = dueDate;

            this.cardMask = cardMask;
            this.totBalIPP = totBalIPP;
            this.vip = vip;

            this.cifVip = cifVip;
            this.idStatement = idStatement;
        }

        public string getSDate()
        {
            return this.sdate;
        }

        public void setSDate(string sDate)
        {
            this.sdate = sDate;
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

        public string getCardBrn()
        {
            return this.cardBrn;
        }

        public void setCardBrn(string cardBrn)
        {
            this.cardBrn = cardBrn;
        }

        public string getStateMonth()
        {
            return this.stateMonth;
        }

        public void setStateMonth(string stateMonth)
        {
            this.stateMonth = stateMonth;
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

        public string getCardMask()
        {
            return this.cardMask;
        }

        public void setCardMask(string cardMask)
        {
            this.cardMask = cardMask;
        }

        public string getTotBalIPP()
        {
            return this.totBalIPP;
        }

        public void setTotBalIPP(string totBalIPP)
        {
            this.totBalIPP = totBalIPP;
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

        public string getIdStatement()
        {
            return this.idStatement;
        }

        public void setIdStatement(string idStatement)
        {
            this.idStatement = idStatement;
        }
    }
}

