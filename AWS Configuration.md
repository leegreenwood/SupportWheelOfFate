# AWS PaaS Configuration

# API Gateway

We're using AWS API Gateway to provide an easy to use REST API.

Current API base URL (Production): https://fj7w0figk9.execute-api.us-east-1.amazonaws.com/prod

# API Gateway - GET List of all Engineers

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

# API Gateway - GET Assigned Engineers

Description: Executes a Lambda Function to return the assigned Engineers for a provided date.

Requirements:
- Lambda Function

## /engineers/{supportDate} - GET Assigned Engineers for a Date

Context-Type: application/json
Body Mapping Template:

`{
    "SupportDate": "$input.params('supportDate')"
}`    
