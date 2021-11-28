# M-Kopa Assesment
This is a simple microservice wrapper which can send messages to one or more SMS providers based on a country. This sending is done async with the use of an inMemory Message broker. This can be extended to use any message broker, RabbitMq or AzureService Bus.

There are to basic parts two this project, The Consumer and The Sender. To keep this as simple as possible I decided to go with an inMemory Message broker so this project can run on any computer.

[The Sender](https://github.com/liamkohncoffey/MKopaAssesment/blob/main/SmsService/SmsSender.cs) is a background process which sends two message to the message broker every 5 seconds.

[The Consumer](https://github.com/liamkohncoffey/MKopaAssesment/blob/main/SmsService.Integration.Consumers/SendSmsCommandConsumer.cs) uses the mass transit library to receive SendSmsCommands message from the SmsService queue. 

Queue type: Fanout

## Interpretation of Acceptance Criteria:
- async event-based and message-based communication.
- different SMS service providers per country
- when a message is 'successfully sent' publish an SmsSentEvent
- Must have logger
- SMS must reliably be sent to customers.
- Retrying should be used
- Sending the same message twice is allowed

## Interpretation of out of scope:
- Sending an http request to a 3rd party

## How To Run

Run following commands in the terminal:

`nuget restore`

`Dotnet clean`

`Dotnet build`

`Dotnet Test`

`Dotnet Run --project SmsService`

### Framework: 
- `.net core 5.0`
### Package Dependancies: 
- `massTransit`
- `NUnit`

