using System;
using alfrek.api.Storage.Interfaces;
using Amazon;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Amazon.S3;
using Amazon.S3.Model;

namespace alfrek.api.Storage
{
    public class AwsStorage : ICloudStorage
    {
        private readonly AmazonS3Client _s3Client;
        
        public AwsStorage()
        {
            var chain = new CredentialProfileStoreChain();
            AWSCredentials awsCredentials;
            //TODO: Remove hardcoded Profile name, refactor to appsettings
            if (chain.TryGetAWSCredentials("Development", out awsCredentials))
            {
                _s3Client = new AmazonS3Client(awsCredentials, RegionEndpoint.EUCentral1);
            }
        }
        
        public async void ListStorage()
        {
            Console.WriteLine("listing buckets");
            try
            {
                ListBucketsResponse response = await _s3Client.ListBucketsAsync();
                foreach (var bucket in response.Buckets)
                {
                    Console.WriteLine("Bucket: " + bucket.BucketName);
                }
            }
            catch (AmazonS3Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}