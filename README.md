# SupportWheel of Fate - Technical Test

This repository contains open source code for a technical test requiring the user to build a tool for selecting enginers to provide technical support for an online software product.

## Initial Commercial Context

The following assumptions guide the decision making of this software project, and should be borne in mind when reviewing the code.

- This project is for internal use only, by technical members of staff
- This project is not revenue-generating
- This project should leverage Platform-as-a-Service principles where possible to simplify the codebase
- Support shifts are for weekdays only, weekends are covered by a separate overseas support team
- 10 shifts per week, 4 hours each, from an initial pool of 10 developers

## Initial Technical Context

- Avoid third-party dependencies where possible
- Minimize the layers of code that need to be maintained
- Potentially support multiple client applications

# Current Business Rules Implementation

The business rules for the current project are as follows:

- An engineer should do at most one half day in a shift
- An engineer cannot have half day shifts on consecutive days
- Each engineer should have completed one whole day of support in a 2 week period

# Current State of Play (29/10/2017)

I have completed the Technical Test to the best of my ability, in the time available (roughly 1.5 man days).

The API has been built using AWS API Gateway, allowing the deployment of a secure REST API without requiring any code. Most methods directly proxy to AWS DynamoDB, except the method for checking the assigned engineers - This is a simple AWS Lambda Function, written in C# on .NET Core 1.0.

The front-end is an Alexa Skill, which can be replicated using the speech Model included in this repository. The Alexa Skill invokes a second AWS Lambda Function (again C# on .NET Core 1.0) which handles the request for assigned engineers on a specific date and calls the API for its information.

I have implemented the Business Rules in a very simplistic fashion, within the API Lambda Function - A formal BRE would be nice but for the purposes of this exercise was considered overcomplicated and over-engineered.

Likewise, I have steered clear of an ORM for the database, which is NoSQL and not an RDBMS due to the simplicity required.

## Next Tasks

Given the time to extend this project, priorities would be as follows:

- Extend the API Gateway implementation with full CRUD methods
- Extend the Speech model for the Alexa Skill
- Increase the number of Unit tests for the Lambda Functions
- Write an independent set of Unit Tests for the AssignEngineerService code
- Modify the assignation algorithm to weight engineers based on how long it has been since their last shift (the longer, the more likely the selection)
- Add a randomizer to the Engineer handle code
