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
- An engineer cannot have half day shifts on ceonsecutive days
- Each engineer should have completed one whole day of support in a 2 week period
