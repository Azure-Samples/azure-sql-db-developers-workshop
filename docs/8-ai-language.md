![A picture of the Microsoft Logo](./media/graphics/microsoftlogo.png)

# Using Azure AI Language

![A picture of the AI Language logo](./media/ch8/lang1.png)

## Azure AI Language

Azure AI Language is a cloud-based service that provides Natural Language Processing (NLP) features for understanding and analyzing text. Use this service to help build intelligent applications using the web-based Language Studio, REST APIs, and client libraries.

### Available features

Language Studio enables you to use the below service features without needing to write code.

| AI Language Feature        | Description    |
| -------------------------- | ------------- |
| Named Entity Recognition (NER) | Named entity recognition is a preconfigured feature that categorizes entities (words or phrases) in unstructured text across several predefined category groups. For example: people, events, places, dates, and more.|
| Personally identifying (PII) and health (PHI) information detection | PII detection is a preconfigured feature that identifies, categorizes, and redacts sensitive information in both unstructured text documents, and conversation transcripts. For example: phone numbers, email addresses, forms of identification, and more.|
| Language detection | Language detection is a preconfigured feature that can detect the language a document is written in, and returns a language code for a wide range of languages, variants, dialects, and some regional/cultural languages.|
| Sentiment Analysis and opinion mining | Sentiment analysis and opinion mining are preconfigured features that help you find out what people think of your brand or topic by mining text for clues about positive or negative sentiment, and can associate them with specific aspects of the text.|
| Summarization | Summarization is a preconfigured feature that uses extractive text summarization to produce a summary of documents and conversation transcriptions. It extracts sentences that collectively represent the most important or relevant information within the original content.|
| Key phrase extraction | Key phrase extraction is a preconfigured feature that evaluates and returns the main concepts in unstructured text, and returns them as a list.|
| Entity linking | Entity linking is a preconfigured feature that disambiguates the identity of entities (words or phrases) found in unstructured text and returns links to Wikipedia.|
| Text analytics for health | Text analytics for health is a preconfigured feature that extracts and labels relevant medical information from unstructured texts such as doctor's notes, discharge summaries, clinical documents, and electronic health records.|
| Custom text classification | Custom text classification enables you to build custom AI models to classify unstructured text documents into custom classes you define.|
| Custom Named Entity Recognition (Custom NER) | Custom NER enables you to build custom AI models to extract custom entity categories (labels for words or phrases), using unstructured text that you provide.|
| Conversational language understanding | Conversational language understanding (CLU) enables users to build custom natural language understanding models to predict the overall intention of an incoming utterance and extract important information from it.|
| Orchestration workflow | Orchestration workflow is a custom feature that enables you to connect Conversational Language Understanding (CLU), question answering, and LUIS applications.|
| Question answering | Question answering is a custom feature that finds the most appropriate answer for inputs from your users, and is commonly used to build conversational client applications, such as social media applications, chat bots, and speech-enabled desktop applications.|
| Custom text analytics for health | Custom text analytics for health is a custom feature that extract healthcare specific entities from unstructured text, using a model you create.|

**Table 1:** Azure AI Language Features

# Getting started with Azure AI Language and REST in the Azure SQL Database

In this section, you will use Azure AI Language with the External REST Endpoint Invocation (EREI) feature of the database to call various endpoints to see how data in the database can be paired with AI features.

## PII and Redaction

The first endpoint we will use is the Personally Identifiable Information (PII) detection service. The PII detection feature can identify, categorize, and redact sensitive information in unstructured text. For example: phone numbers, email addresses, and forms of identification. 

1. Copy the following SQL and paste it into the SQL query editor.

    ```SQL
    declare @url nvarchar(4000) = N'https://vslive2024language.cognitiveservices.azure.com/language/:analyze-text?api-version=2023-04-01';
    declare @headers nvarchar(300) = N'{"Ocp-Apim-Subscription-Key":"LANGUAGE_KEY"}';
    declare @payload nvarchar(max) = N'{
        "kind": "PiiEntityRecognition",
        "analysisInput":
        {
            "documents":
            [
                {
                    "id":"1",
                    "language": "en",
                    "text": "abcdef@abcd.com, this is my phone is 6657789887, and my IP: 255.255.255.255 127.0.0.1 fluffybunny@bunny.net, My Addresses are 1 Microsoft Way, Redmond, WA 98052, SSN 543-55-6654, 123 zoo street chickenhouse, AZ 55664"
                }
            ]
        }
    }';

    declare @ret int, @response nvarchar(max);

    exec @ret = sp_invoke_external_rest_endpoint 
        @url = @url,
        @method = 'POST',
        @headers = @headers,
        @payload = @payload,
        @timeout = 230,
        @response = @response output;

    select @ret as ReturnCode, @response as Response;
    ```

1. Replace the **LANGUAGE_KEY** text with the AI Language Key that was returned to you in the previous chapter when testing connectivity.

1. Execute the SQL statement with the run button.

1. View the return message. Points you will want to examine are where the text came back with all PII redacted and the section where each piece of PII is categorized.

    ```JSON
    "redactedText": "***************, this is my phone is **********, and my IP: *************** ********* *********************, My Addresses are **********************************, SSN ***********, *************************************",
    "id": "1",
    "entities": [
        {
            "text": "abcdef@abcd.com",
            "category": "Email",
            "offset": 0,
            "length": 15,
            "confidenceScore": 0.8
        },
        {
            "text": "6657789887",
            "category": "EUTaxIdentificationNumber",
            "offset": 37,
            "length": 10,
            "confidenceScore": 0.93
        },
        {
            "text": "255.255.255.255",
            "category": "IPAddress",
            "offset": 60,
            "length": 15,
            "confidenceScore": 0.8
        },
        {
            "text": "127.0.0.1",
            "category": "IPAddress",
            "offset": 76,
            "length": 9,
            "confidenceScore": 0.8
        },
        {
            "text": "fluffybunny@bunny.net",
            "category": "Email",
            "offset": 86,
            "length": 21,
            "confidenceScore": 0.8
        },
        {
            "text": "1 Microsoft Way, Redmond, WA 98052",
            "category": "Address",
            "offset": 126,
            "length": 34,
            "confidenceScore": 1.0
        },
        {
            "text": "543-55-6654",
            "category": "USSocialSecurityNumber",
            "offset": 166,
            "length": 11,
            "confidenceScore": 0.85
        },
        {
            "text": "123 zoo street chickenhouse, AZ 55664",
            "category": "Address",
            "offset": 179,
            "length": 37,
            "confidenceScore": 0.95
        }
    ],
    ```

### Answer Questions

The Answer Questions capability attempts to extract the answer to a given question from the passage of text provided. Extract questions and answers from your semi-structured content, including FAQs, manuals, database data, and documents.

1. Copy the following SQL and paste it into the SQL query editor. This example uses a description from the Adventure Works ProductDescription table to seed the session.

    ```SQL
    declare @url nvarchar(4000) = N'https://languagebuild2024.cognitiveservices.azure.com/language/:query-text?api-version=2023-04-01';
    declare @headers nvarchar(300) = N'{"Ocp-Apim-Subscription-Key":"LANGUAGE_KEY"}';
    declare @message nvarchar(max);
    SET @message = (SELECT [Description]
                    FROM [SalesLT].[ProductDescription]
                    WHERE ProductDescriptionID = 457);
    declare @payload nvarchar(max) = N'{
    "question": "What is the bike made from?",
    "records": [
        {
        "id": "1",
        "text": "'+ @message +'"
        }
    ],
    "language": "en"
    }';

    declare @ret int, @response nvarchar(max);

    exec @ret = sp_invoke_external_rest_endpoint 
        @url = @url,
        @method = 'POST',
        @headers = @headers,
        @payload = @payload,
        @timeout = 230,
        @response = @response output;

    select @ret as ReturnCode, @response as Response;
    ```

1. Replace the **LANGUAGE_KEY** text with the AI Language Key that was returned to you in the previous chapter when testing connectivity.

1. Execute the SQL statement with the run button.

1. View the return message. You can see the confidence score for each answer as to how it feels it performed based on the text provided and the question asked.

    ```JSON
    "answers": [
      {
        "answer": "Developed with the Adventure Works Cycles professional race team, it has a extremely light heat-treated aluminum frame, and steering that allows precision control.",
        "confidenceScore": 0.5158345103263855,
        "id": "1",
        "answerSpan": {
          "text": "aluminum",
          "confidenceScore": 0.8578997,
          "offset": 103,
          "length": 9
        },
        "offset": 37,
        "length": 163
      },
      {
        "answer": "This bike is ridden by race winners. Developed with the Adventure Works Cycles professional race team, it has a extremely light heat-treated aluminum frame, and steering that allows precision control.",
        "confidenceScore": 0.4569839537143707,
        "id": "1",
        "answerSpan": {
          "text": "aluminum",
          "confidenceScore": 0.8340067,
          "offset": 140,
          "length": 9
        },
        "offset": 0,
        "length": 200
      }
    ]
    ```    

### Document summarization

This prebuilt summarization API can produce a summary for a conversation or from a document. This is a two step process where we first submit the text as a job then get the results with the **returned job request ID**.

1. Copy the following SQL and paste it into the SQL query editor.

    ```SQL
    declare @url nvarchar(4000) = N'https://languagebuild2024.cognitiveservices.azure.com/language/analyze-text/jobs?api-version=2023-04-01';
    declare @headers nvarchar(300) = N'{"Ocp-Apim-Subscription-Key":"LANGUAGE_KEY"}';
    declare @payload nvarchar(max) = N'{
    "displayName": "Document ext Summarization Task Example",
    "analysisInput": {
        "documents": [
        {
            "id": "1",
            "language": "en",
            "text": "REDMOND, Wash. and Santa Monica, Calif. Jan. 18, 2022 With three billion people actively playing games today, and fueled by a new generation steeped in the joys of interactive entertainment, gaming is now the largest and fastest-growing form of entertainment. Today, Microsoft Corp. (Nasdaq: MSFT) announced plans to acquire Activision Blizzard Inc. (Nasdaq: ATVI), a leader in game development and interactive entertainment content publisher. This acquisition will accelerate the growth in Microsofts gaming business across mobile, PC, console and cloud and will provide building blocks for the metaverse. Microsoft will acquire Activision Blizzard for 95.00 per share, in an all-cash transaction valued at $68.7 billion, inclusive of Activision Blizzards net cash. When the transaction closes, Microsoft will become the worlds third-largest gaming company by revenue, behind Tencent and Sony. The planned acquisition includes iconic franchises from the Activision, Blizzard and King studios like Warcraft, Diablo, Overwatch, Call of Duty and Candy Crush, in addition to global eSports activities through Major League Gaming. The company has studios around the world with nearly 10,000 employees. Bobby Kotick will continue to serve as CEO of Activision Blizzard, and he and his team will maintain their focus on driving efforts to further strengthen the companys culture and accelerate business growth. Once the deal closes, the Activision Blizzard business will report to Phil Spencer, CEO, Microsoft Gaming. Gaming is the most dynamic and exciting category in entertainment across all platforms today and will play a key role in the development of metaverse platforms, said Satya Nadella, chairman and CEO, Microsoft. Were investing deeply in world-class content, community and the cloud to usher in a new era of gaming that puts players and creators first and makes gaming safe, inclusive and accessible to all. Players everywhere love Activision Blizzard games, and we believe the creative teams have their best work in front of them, said Phil Spencer, CEO, Microsoft Gaming. Together we will build a future where people can play the games they want, virtually anywhere they want. For more than 30 years our incredibly talented teams have created some of the most successful games, said Bobby Kotick, CEO, Activision Blizzard. The combination of Activision Blizzards world-class talent and extraordinary franchises with Microsofts technology, distribution, access to talent, ambitious vision and shared commitment to gaming and inclusion will help ensure our continued success in an increasingly competitive industry. Mobile is the largest segment in gaming, with nearly 95 of all players globally enjoying games on mobile. Through great teams and great technology, Microsoft and Activision Blizzard will empower players to enjoy the most-immersive franchises, like Halo and Warcraft, virtually anywhere they want. And with games like Candy Crush, Activision Blizzards mobile business represents a significant presence and opportunity for Microsoft in this fast-growing segment. The acquisition also bolsters Microsofts Game Pass portfolio with plans to launch Activision Blizzard games into Game Pass, which has reached a new milestone of over 25 million subscribers. With Activision Blizzards nearly 400 million monthly active players in 190 countries and three billion-dollar franchises, this acquisition will make Game Pass one of the most compelling and diverse lineups of gaming content in the industry. Upon close, Microsoft will have 30 internal game development studios, along with additional publishing and esports production capabilities. The transaction is subject to customary closing conditions and completion of regulatory review and Activision Blizzards shareholder approval. The deal is expected to close in fiscal year 2023 and will be accretive to non-GAAP earnings per share upon close. The transaction has been approved by the boards of directors of both Microsoft and Activision Blizzard."
        }
        ]
    },
    "tasks": [
        {
        "kind": "ExtractiveSummarization",
        "taskName": "Document Extractive Summarization Task 1",
        "parameters": {
            "sentenceCount": 6
        }
        }
    ]
    }';

    declare @ret int, @response nvarchar(max);

    exec @ret = sp_invoke_external_rest_endpoint 
        @url = @url,
        @method = 'POST',
        @headers = @headers,
        @payload = @payload,
        @timeout = 230,
        @response = @response output;

    select @ret as ReturnCode, @response as Response;
    ```

1. Replace the **LANGUAGE_KEY** text with the AI Language Key that was returned to you in the previous chapter when testing connectivity.

1. Execute the SQL statement with the run button.

1. View the results and find the **operation-location** value.

    ```JSON
    {
        "response": {
            "status": {
                "http": {
                    "code": 202,
                    "description": ""
                }
            },
            "headers": {
                "Date": "Fri, 03 May 2024 20:31:10 GMT",
                "Content-Length": "0",
                "operation-location": "https://languagebuild2024.cognitiveservices.azure.com/language/analyze-text/jobs/1111-2222-4444-111?api-version=2023-04-01",
                "x-envoy-upstream-service-time": "238",
                "apim-request-id": "abababab-abab-1234-1122-abababababab",
                "strict-transport-security": "max-age=31536000; includeSubDomains; preload",
                "x-content-type-options": "nosniff",
                "x-ms-region": "West US"
            }
        }
    }
    ```

1. Copy the URL in the **operation-location** value.

1. Copy and paste the following code into the query editor.

    ```SQL
    declare @url nvarchar(4000) = N'OPERATION-LOCATION-URL';
    declare @headers nvarchar(300) = N'{"Ocp-Apim-Subscription-Key":"LANGUAGE_KEY"}';
    declare @ret int, @response nvarchar(max);

    exec @ret = sp_invoke_external_rest_endpoint 
        @url = @url,
        @method = 'GET',
        @headers = @headers,
        @timeout = 230,
        @response = @response output;

    select @ret as ReturnCode, @response as Response;
    ```

1. Replace the **LANGUAGE_KEY** text with the AI Language Key that was returned to you in the previous chapter when testing connectivity. Replace the **OPERATION-LOCATION-URL** with the **operation-location** URL value from the response message.

1. Execute the SQL statement with the run button.

1. View the return message. You will see that the service took out the key phrases and listed them in the response message.

    ```JSON
    "sentences": [
    {
        "text": "Today, Microsoft Corp. (Nasdaq: MSFT) announced plans to acquire Activision Blizzard Inc. (Nasdaq: ATVI), a leader in game development and interactive entertainment content publisher.",
        "rankScore": 0.9,
        "offset": 260,
        "length": 183
    },
    {
        "text": "This acquisition will accelerate the growth in Microsofts gaming business across mobile, PC, console and cloud and will provide building blocks for the metaverse.",
        "rankScore": 0.83,
        "offset": 444,
        "length": 162
    },
    {
        "text": "Microsoft will acquire Activision Blizzard for 95.00 per share, in an all-cash transaction valued at $68.7 billion, inclusive of Activision Blizzards net cash.",
        "rankScore": 1.0,
        "offset": 607,
        "length": 159
    },
    {
        "text": "When the transaction closes, Microsoft will become the worlds third-largest gaming company by revenue, behind Tencent and Sony.",
        "rankScore": 0.98,
        "offset": 767,
        "length": 127
    },
    {
        "text": "The planned acquisition includes iconic franchises from the Activision, Blizzard and King studios like Warcraft, Diablo, Overwatch, Call of Duty and Candy Crush, in addition to global eSports activities through Major League Gaming.",
        "rankScore": 0.63,
        "offset": 895,
        "length": 231
    },
    {
        "text": "The company has studios around the world with nearly 10,000 employees.",
        "rankScore": 0.52,
        "offset": 1127,
        "length": 70
    }
    ```

### Sentiment analysis

Azure AI Language Sentiment Analysis feature provides sentiment labels (such as "negative", "neutral" and "positive") and confidence scores at the sentence and document-level. You can also send Opinion Mining requests using the Sentiment Analysis endpoint, which provides granular information about the opinions related to words (such as the attributes of products or services) in the text.

1. Copy the following SQL and paste it into the SQL query editor.

    ```SQL
    declare @url nvarchar(4000) = N'https://languagebuild2024.cognitiveservices.azure.com/language/:analyze-text?api-version=2023-04-01';
    declare @headers nvarchar(300) = N'{"Ocp-Apim-Subscription-Key":"LANGUAGE_KEY"}';
    declare @payload nvarchar(max) = N'{
        "kind": "SentimentAnalysis",
        "parameters": {
            "modelVersion": "latest",
            "opinionMining": "False"
        },
        "analysisInput":{
            "documents":[
                {
                    "id":"1",
                    "language":"en",
                    "text": "The food and service were unacceptable. The concierge was nice, however."
                }
            ]
        }
    } ';

    declare @ret int, @response nvarchar(max);

    exec @ret = sp_invoke_external_rest_endpoint 
        @url = @url,
        @method = 'POST',
        @headers = @headers,
        @payload = @payload,
        @timeout = 230,
        @response = @response output;

    select @ret as ReturnCode, @response as Response;
    ```

1. Replace the **LANGUAGE_KEY** text with the AI Language Key that was returned to you in the previous chapter when testing connectivity.

1. Execute the SQL statement with the run button.

1. View the return message. You can see that the overall message was mixed and it classified one positive and one negative sentence in the text.

    ```JSON
    "documents": [
    {
        "id": "1",
        "sentiment": "mixed",
        "confidenceScores": {
            "positive": 0.43,
            "neutral": 0.04,
            "negative": 0.53
        },
        "sentences": [
            {
                "sentiment": "negative",
                "confidenceScores": {
                    "positive": 0.0,
                    "neutral": 0.01,
                    "negative": 0.99
                },
                "offset": 0,
                "length": 40,
                "text": "The food and service were unacceptable. "
            },
            {
                "sentiment": "positive",
                "confidenceScores": {
                    "positive": 0.86,
                    "neutral": 0.08,
                    "negative": 0.07
                },
                "offset": 40,
                "length": 32,
                "text": "The concierge was nice, however."
            }
        ],
        "warnings": []
    }
    ```

1. If you want to see how the endpoint can expand its classification to key words in it evaluation, change **opinionMining** to **True** and execute the call again.

    ```JSON
    "opinionMining": "True"
    ```

### Language detection

The Language Detection feature of the Azure AI Language REST API evaluates text input for each document and returns language identifiers with a score that indicates the strength of the analysis. This capability is useful for content stores that collect arbitrary text, where language is unknown. You can parse the results of this analysis to determine which language is used in the input document. The response also returns a score that reflects the confidence of the model. The score value is between 0 and 1.

1. Copy the following SQL and paste it into the SQL query editor.

    ```SQL
    declare @message nvarchar(max);
    SET @message = (SELECT top 1 d.[Description]
                    FROM [SalesLT].[ProductDescription] d,
                         [SalesLT].[ProductModelProductDescription] l
                    WHERE d.ProductDescriptionID = l.ProductDescriptionID
                    AND l.Culture = 'fr');

    declare @url nvarchar(4000) = N'https://languagebuild2024.cognitiveservices.azure.com/language/:analyze-text?api-version=2023-04-01';
    declare @headers nvarchar(300) = N'{"Ocp-Apim-Subscription-Key":"LANGUAGE_KEY"}';
    declare @payload nvarchar(max) = N'{
        "kind": "LanguageDetection",
        "parameters": {
            "modelVersion": "latest"
        },
        "analysisInput":{
            "documents":[
                {
                    "id":"1",
                    "text": "' + @message + '"
                }
            ]
        }
    }';

    declare @ret int, @response nvarchar(max);

    exec @ret = sp_invoke_external_rest_endpoint 
        @url = @url,
        @method = 'POST',
        @headers = @headers,
        @payload = @payload,
        @timeout = 230,
        @response = @response output;

    select @ret as ReturnCode, @response as Response;
    ```

1. Replace the **LANGUAGE_KEY** text with the AI Language Key that was returned to you in the previous chapter when testing connectivity.

1. Execute the SQL statement with the run button.

1. View the return message. You can see the detected language and the confidence score.

    ```JSON
    "results": {
        "documents": [
            {
                "id": "1",
                "detectedLanguage": {
                    "name": "French",
                    "iso6391Name": "fr",
                    "confidenceScore": 1.0
                },
    ```

1. You can try other languages by just altering the where clause in the SQL statement that set the @message variable. The accepted values you can use are: zh-cht, he, th, fr, ar, and en.     

    ```SQL
        SET @message = (SELECT top 1 d.[Description]
                    FROM [SalesLT].[ProductDescription] d,
                         [SalesLT].[ProductModelProductDescription] l
                    WHERE d.ProductDescriptionID = l.ProductDescriptionID
                    AND l.Culture = 'LANGUAGE-HERE');
    ```

    Replace the **LANGUAGE-HERE** with one of the above values and retry the request.

### Named Entity Recognition (NER)

This prebuilt capability uses Named Entity Recognition (NER) to identify entities in text and categorize them into pre-defined classes or types such as: person, location, event, product, and organization. This request again pulls from the Adventure Works data for text analysis.

1. Copy the following SQL and paste it into the SQL query editor.

    ```SQL
    declare @message nvarchar(max);
    SET @message = (SELECT [Description]
        FROM [SalesLT].[ProductDescription]
        WHERE ProductDescriptionID = 513);

    declare @url nvarchar(4000) = N'https://languagebuild2024.cognitiveservices.azure.com/language/:analyze-text?api-version=2023-04-01';
    declare @headers nvarchar(300) = N'{"Ocp-Apim-Subscription-Key":"LANGUAGE_KEY"}';
    declare @payload nvarchar(max) = N'{
        "kind": "EntityRecognition",
        "parameters": {
            "modelVersion": "latest"
        },
        "analysisInput":{
            "documents":[
                {
                    "id":"1",
                    "language": "en",
                    "text": "' + @message + '"
                }
            ]
        }
    }';

    declare @ret int, @response nvarchar(max);

    exec @ret = sp_invoke_external_rest_endpoint 
        @url = @url,
        @method = 'POST',
        @headers = @headers,
        @payload = @payload,
        @timeout = 230,
        @response = @response output;

    select @ret as ReturnCode, @response as Response;
    ```

1. Replace the **LANGUAGE_KEY** text with the AI Language Key that was returned to you in the previous chapter when testing connectivity.

1. Execute the SQL statement with the run button.

1. View the return message. You can see how it extracted key words and classified them (with some have multiple levels of classification).

    ```JSON
    "entities": [
        {
            "text": "bike",
            "category": "Product",
            "offset": 19,
            "length": 4,
            "confidenceScore": 0.92
        },
        {
            "text": "tires",
            "category": "Product",
            "offset": 94,
            "length": 5,
            "confidenceScore": 0.98
        },
        {
            "text": "town",
            "category": "Location",
            "subcategory": "Structural",
            "offset": 118,
            "length": 4,
            "confidenceScore": 0.75
        },
        {
            "text": "weekend",
            "category": "DateTime",
            "subcategory": "DateRange",
            "offset": 126,
            "length": 7,
            "confidenceScore": 0.97
        }
    ],
    ```

1. If you want to experiment with this, you can change the **ProductDescriptionID** in the SQL statement that sets the message. Some values you can use are 661, 1062, or 647.

### Entity Linking

This prebuilt capability disambiguates the identity of an entity found in text by linking to a Wikipedia article.

1. Copy the following SQL and paste it into the SQL query editor.

    ```SQL
    declare @message nvarchar(max);
    SET @message = (SELECT [Description]
        FROM [SalesLT].[ProductDescription]
        WHERE ProductDescriptionID = 168);

    declare @url nvarchar(4000) = N'https://languagebuild2024.cognitiveservices.azure.com/language/:analyze-text?api-version=2023-04-01';
    declare @headers nvarchar(300) = N'{"Ocp-Apim-Subscription-Key":"LANGUAGE_KEY"}';
    declare @payload nvarchar(max) = N'{
        "kind": "EntityLinking",
        "parameters": {
            "modelVersion": "latest"
        },
        "analysisInput":{
            "documents":[
                {
                    "id":"1",
                    "language":"en",
                    "text": " ' + @message + '"
                }
            ]
        }
    }';

    declare @ret int, @response nvarchar(max);

    exec @ret = sp_invoke_external_rest_endpoint 
        @url = @url,
        @method = 'POST',
        @headers = @headers,
        @payload = @payload,
        @timeout = 230,
        @response = @response output;

    select @ret as ReturnCode, @response as Response;
    ```

1. Replace the **LANGUAGE_KEY** text with the AI Language Key that was returned to you in the previous chapter when testing connectivity.

1. Execute the SQL statement with the run button.

1. View the return message.

    ```JSON
    "entities": [
        {
            "bingId": "75ea1e02-58a4-a45f-232d-e7a729d644a6",
            "name": "Vehicle frame",
            "matches": [
                {
                    "text": "Frame",
                    "offset": 100,
                    "length": 5,
                    "confidenceScore": 0.04
                }
            ],
            "language": "en",
            "id": "Vehicle frame",
            "url": "https://en.wikipedia.org/wiki/Vehicle_frame",
            "dataSource": "Wikipedia"
        }
    ```
