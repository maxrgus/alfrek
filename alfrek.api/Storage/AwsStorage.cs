using System;
using alfrek.api.Configuration;
using alfrek.api.Storage.Interfaces;
using Amazon;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Options;

namespace alfrek.api.Storage
{
    public class AwsStorage : ICloudStorage
    {
        private readonly AmazonS3Client _s3Client;
        private readonly IOptions<AwsConfiguration> _configuration;
        
        public AwsStorage(IOptions<AwsConfiguration> configuration)
        {
            _configuration = configuration;
            
            var chain = new CredentialProfileStoreChain();
            AWSCredentials awsCredentials;
            if (chain.TryGetAWSCredentials(_configuration.Value.ProfileName, out awsCredentials))
            {
                _s3Client = new AmazonS3Client(awsCredentials, RegionEndpoint.EUCentral1);
            }
            else
            {
                Console.WriteLine("ERROR: FAILED TO CREATE AWS CLIENT");
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
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}