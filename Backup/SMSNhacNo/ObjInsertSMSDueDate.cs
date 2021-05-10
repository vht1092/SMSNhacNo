using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMSNhacNo
{
    class ObjInsertSMSDueDate
    {
        private string id; //SMS_TYPE + LOC + KY_SAO_KE
        private string smsType;//DEBT02
        private string smsDetail;
        private string desMobile;

        private string dateTime;//YYYYMMDD HHmmsszzz
        private string insertTransDateTime; //time insert qua db ebank

        private string pan;//so the ma hoa = maskCard;
        private string cardBrn; //VS, MC
        private string cardType; //G, W, 

        private string smsMonth; //NUMBER(6,0) YYYYMM
        private string closingBalance;
        private string dueDate;
        private string minimumPayment;
        private string actionType; //N: tao moi, Y: da gui qua eb, F: gui qua eb failed, D: ko gui
        private string totalBalIpp;

        private string vip;
        private string cifVip;
        private string cardNo; //so the che
        private string loc;
        private string idStatement;

        public ObjInsertSMSDueDate() { }

        public ObjInsertSMSDueDate(string id, string smsType, string smsDetail, string desMobile,
            string dateTime, string insertTransDateTime, string pan, string cardBrn, string cardType,
            string smsMonth, string closingBalance, string dueDate, string minimumPayment,
            string actionType, string totalBalIpp, string vip, string cifVip, string cardNo,
            string loc, string idStatement)
        {
            this.id = id;
            this.smsType = smsType;
            this.smsDetail = smsDetail;
            this.desMobile = desMobile;

            this.dateTime = dateTime;
            this.insertTransDateTime = insertTransDateTime;
	        this.pan = pan;
            this.cardBrn = cardBrn; //VS, MC
            this.cardType = cardType; //G, W, 
            this.smsMonth = smsMonth; //NUMBER(6,0), 
            this.closingBalance = closingBalance;
            this.dueDate = dueDate;
            this.minimumPayment = minimumPayment;
            this.actionType = actionType;
            this.totalBalIpp = totalBalIpp;

            this.vip = vip;
            this.cifVip = cifVip;
            this.cardNo = cardNo;
            this.loc = loc;
            this.idStatement = idStatement;
        }

        public string getId()
        {
            return this.id;
        }

        public void setId(string id)
        {
            this.id = id;
        }

        public string getSmsType()
        {
            return this.smsType;
        }

        public void setSmsType(string smsType)
        {
            this.smsType = smsType;
        }

        public string getSmsDetail()
        {
            return this.smsDetail;
        }

        public void setSmsDetail(string smsDetail)
        {
            this.smsDetail = smsDetail;
        }

        public string getDesMobile()
        {
            return this.desMobile;
        }

        public void setDesMobile(string desMobile)
        {
            this.desMobile = desMobile;
        }

        public string getDateTime()
        {
            return this.dateTime;
        }

        public void setDateTime(string dateTime)
        {
            this.dateTime = dateTime;
        }

        public string getInsertTransDateTime()
        {
            return this.insertTransDateTime;
        }

        public void setInsertTransDateTime(string insertTransDateTime)
        {
            this.insertTransDateTime = insertTransDateTime;
        }

        public string getPan()
        {
            return this.pan;
        }

        public void setPan(string pan)
        {
            this.pan = pan;
        }

        public string getCardBrn()
        {
            return this.cardBrn;
        }

        public void setCardBrn(string cardBrn)
        {
            this.cardBrn = cardBrn;
        }

        public string getCardType()
        {
            return this.cardType;
        }

        public void setCardType(string cardType)
        {
            this.cardType = cardType;
        }

        public string getSmsMonth()
        {
            return this.smsMonth;
        }

        public void setSmsMonth(string smsMonth)
        {
            this.smsMonth = smsMonth;
        }

        public string getClosingBalance()
        {
            return this.closingBalance;
        }

        public void setClosingBalance(string closingBalance)
        {
            this.closingBalance = closingBalance;
        }

        public string getDueDate()
        {
            return this.dueDate;
        }

        public void setDueDate(string dueDate)
        {
            this.dueDate = dueDate;
        }

        public string getMinimumPayment()
        {
            return this.minimumPayment;
        }

        public void setMinimumPayment(string minimumPayment)
        {
            this.minimumPayment = minimumPayment;
        }

        public string getActionType()
        {
            return this.actionType;
        }

        public void setActionType(string actionType)
        {
            this.actionType = actionType;
        }

        public string getTotalBalIpp()
        {
            return this.totalBalIpp;
        }

        public void setTotalBalIpp(string totalBalIpp)
        {
            this.totalBalIpp = totalBalIpp;
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

        public string getCardNo()
        {
            return this.cardNo;
        }

        public void setCardNo(string cardNo)
        {
            this.cardNo = cardNo;
        }

        public string getLoc()
        {
            return this.loc;
        }

        public void setLoc(string loc)
        {
            this.loc = loc;
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
/*
private string id;  //SMS_TYPE + LOC + KY_SAO_KE
private string smsType;
private string smsDetail;
private string desMobile;

private string dateTime;
private string insertTransDateTime; //time insert qua db ebank
private string pan;//so the ma hoa
private string cardBrn; //VS, MC
private string cardType; //G, W, 
private string smsMonth; //NUMBER(6,0) YYYYMM
private string closingBalance;
private string dueDate;
private string minimumPayment;
private string actionType; //N: tao moi, Y: da gui qua eb, F: gui qua eb failed, D: ko gui
private string totalBalIpp;

private string vip;
private string cifVip;
private string cardNo; //so the che
private string loc;
private string idStatement;

//--------------------------------------------------------------
private string sysDate;
private string handPhone;
private string cardNo; 
private string cardType;
private string cardBrand;
private string statementMonth;
private string closingBalance;
private string minimumPayment;
private string dueDate;
private string maskCard; //=pan
private string totalBalanceIPP;
private string vip;
private string cifVip;
private string sumPaymentCw;
*/