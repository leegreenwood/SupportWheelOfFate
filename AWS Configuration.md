# AWS PaaS Configuration

# API Gateway

We're using AWS API Gateway to provide an easy to use REST API.

Current API base URL: https://fj7w0figk9.execute-api.us-east-1.amazonaws.com/prod

# API Gateway - GET list of Engineers

Description: Proxies DynamoDB to run a Scan Operation to return all Engineers.

Requirements:
- Valid IAM Role with permissions to DynamoDB

## /engineers - GET - Integration Request

Context-Type: application/json
Body Mapping Template:

`{
    "TableName": "SupportWheelOfFate"
}`

## /engineers - GET - Integration Response

Context-Type: application/json
Body Mapping Template:

`#set($inputRoot = $input.path('$'))
#foreach($elem in $inputRoot.Items)
{    
    "engineerId": "$elem.engineerId.S",
    "engineerName": "$elem.engineerName.S",
    "engineerHandle": "$elem.engineerHandle.S",
    "dateLastShift": "$elem.dateLastShift.S",
    "timeLastShift": "$elem.timeLastShift.S"
}#if($foreach.hasNext),#end
#end`
