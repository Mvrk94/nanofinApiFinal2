﻿using MultiChainLib.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNanoFinAPI.MultiChainLib.Controllers;

namespace MultiChainLib.Controllers
{
    public class MResellerController
    {
        MultiChainClient client;
        MUserController user;
        string nanoFinAddr;
        string burnAddress;
        public MResellerController(int resellerUserID)
        {
            //client = new MultiChainClient("188.166.170.248", 6492, false, "multichainrpc", "AYBR44NDe7VdSWXHJCrR2i2xhCcnByorzHy6f6vaczTd", "NanoFinBlockChain");
            client = new MultiChainClient("188.166.170.248", 2748, false, "multichainrpc", "7yPU3yrroGZp4WAgrL2cD9JDe7WbwwiUmLps3PPmPPde", "NanoFinBlockchain");
            user = new MUserController(resellerUserID, client);
        }

        public async Task<MResellerController> init()
        {
            user = await user.init();
            nanoFinAddr = await MUtilityClass.getAddress(client, 1);
            burnAddress = await MUtilityClass.getBurnAddress(client);
            return this;
        }
        //issue reseller more bulkvoucher  
        public async Task<int> buyBulk(int amount)
        {
            //check if current user has the correct permissions, if not grant permissions
            //await user.grantPermissions(BlockchainPermissions.Receive);
            //issue reseller bulk voucher of amount specified, transaction contains metadata
            var issueMore = await client.IssueMoreFromWithMetadataAsync(nanoFinAddr, user.propertyUserAddress(), "BulkVoucher", amount, "Issue reseller \'" + user.propertyUserID().ToString() + "\' " + amount.ToString() + " BulkVoucher.");
            issueMore.AssertOk();

            return 0;
        }
        //burn reseller bulk voucher. issue consumer voucher
        public async Task<int> sendBulk(int recipientUserID, int amount)
        {
            string recipientAddr = await MUtilityClass.getAddress(client, recipientUserID);
            //await user.grantPermissions(BlockchainPermissions.Send);
            //spend reseller BulkVoucher inputs
            string metadata = "Reseller \'" + user.propertyUserID() + "\' spent " + amount.ToString() + " BulkVoucher. " + amount.ToString() + " Voucher of same amount to be issued to user \'" + recipientUserID.ToString() + "\'.";
            var sendWithMetaDataFrom = await client.SendWithMetadataFromAsync(user.propertyUserAddress(), burnAddress, "BulkVoucher", amount, MUtilityClass.strToHex(metadata));  //metadata has to be converted to hex. convert back to string online or with MUtilityClasss
            sendWithMetaDataFrom.AssertOk();

            //check if recipient has the correct permissions, if not grant permissions
            MUserController recipientUser = new MUserController(recipientUserID);
            recipientUser = await recipientUser.init();
            //await recipientUser.grantPermissions(BlockchainPermissions.Receive);
            //issue recipient voucher
            var issueMore = await client.IssueMoreFromWithMetadataAsync(nanoFinAddr, recipientAddr, "Voucher", amount, "Issue consumer \'" + recipientUserID.ToString() + "\' " + amount.ToString() + " Voucher");
            issueMore.AssertOk();

            return 0;
        }


        public async Task atomicVoucherExchange(int resellerUserID, int bulkVoucherAmount, int UserID, int voucherAmount )
        {

            var jVoucher = new voucherJSON()
            {
                Voucher = voucherAmount
            };

            var jBulkVoucher = new bulkVoucherJSON()
            {
                BulkVoucher = bulkVoucherAmount
            };

            var voucherJsonStr = JsonConvert.SerializeObject(jVoucher.Values);
            var bulkVouckerJsonStr = JsonConvert.SerializeObject(jBulkVoucher.Values);
            await MUtilityClass.atomicExchange(client, resellerUserID, bulkVouckerJsonStr, UserID, voucherJsonStr);
        }


    }
}
