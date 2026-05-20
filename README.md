# Blueink .NET SDK

Official .NET client library for the [Blueink eSignature API](https://developer.blueink.com/docs/api/). This SDK gives you a strongly typed, synchronous interface for working with Blueink API v2 resources such as bundles, packets, persons, templates, webhooks, and rate limits.

It is a good fit for server-side .NET applications that need to:

- send signature requests
- upload documents and place fields
- create embedded signing experiences
- manage contacts and templates
- receive webhook notifications

## Core Concepts

Blueink's API uses a few domain terms that show up throughout this SDK:

- `Bundle`: a signing workflow containing one or more documents and one or more signers
- `Packet`: an individual signer within a bundle
- `Person`: a reusable contact record
- `Template`: a reusable document or envelope definition

The SDK organizes those concepts into resource classes exposed from `BlueinkService`.

## Supported Frameworks

This package targets:

- `.NET Standard 2.0`
- `.NET 8.0`

That means it can be used from modern .NET apps and from older runtimes that support .NET Standard 2.0.

## Installation

Using the .NET CLI:

```bash
dotnet add package Blueink.Client.Net
```

Using `PackageReference`:

```xml
<ItemGroup>
  <PackageReference Include="Blueink.Client.Net" Version="1.0.2" />
</ItemGroup>
```

## Authentication and Configuration

The SDK authenticates with a Blueink API key. By default, `BlueinkService` reads the key from the `BLUEINK_API_KEY` environment variable.

```bash
export BLUEINK_API_KEY="your-api-key"
```

If you need to override the API base URL, you can also set:

```bash
export BLUEINK_API_URL="https://api.blueink.com/api/v2/"
```

You can also pass values directly:

```csharp
using Blueink.Client.Net.v2;

var client = new BlueinkService("your-api-key");
var customClient = new BlueinkService("your-api-key", "https://api.blueink.com/api/v2/");
```

## Quick Start

All resource methods return request objects. Call `Execute()` to send the request and deserialize the response.

```csharp
using System;
using Blueink.Client.Net.v2;

using (var client = new BlueinkService())
{
    var request = client.BundleResource.ListBundles();
    var bundles = request.Execute();

    Console.WriteLine($"Fetched {bundles.Count} bundle(s).");
    Console.WriteLine($"Page {request.CurrentPageNumber} of {request.TotalPagesCount}");
}
```

## Common Examples

### Create a person

```csharp
using System;
using System.Collections.Generic;
using Blueink.Client.Net.v2;

using (var client = new BlueinkService())
{
    var person = client.PersonResource.CreatePerson(
        name: "Jane Signer",
        metadata: new Dictionary<string, string>
        {
            ["customer_id"] = "cust_12345"
        },
        emails: new List<string> { "jane@example.com" },
        phones: new List<string> { "+15551234567" }
    ).Execute();

    Console.WriteLine($"Created person: {person.Id}");
}
```

### Create a bundle from an uploaded PDF

This example uploads a PDF, adds a signer, and places fields using anchor text.

```csharp
using System;
using System.Collections.Generic;
using Blueink.Client.Net.v2;
using Blueink.Client.Net.v2.Helper;
using Blueink.Client.Net.v2.Model;

using (var client = new BlueinkService())
{
    var bundle = new BundleHelper
    {
        Label = "Service Agreement",
        EmailSubject = "Please sign your agreement",
        EmailMessage = "Review and sign when ready.",
        IsTest = true,
        InOrder = true
    };

    var signerKey = bundle.AddSigner(
        name: "Jane Signer",
        email: "jane@example.com",
        phone: null,
        deliveryVia: DeliveryVia.Email,
        order: 0);

    var documentKey = bundle.AddDocumentAndFileToUpload(
        key: "agreement",
        filePath: "./agreement.pdf");

    bundle.AddAutoplacement(
        documentKey: documentKey,
        kind: FieldKind.ESignature,
        search: "[SIGN_HERE]",
        w: 160,
        h: 48,
        offsetX: 0,
        offsetY: 0,
        editors: new List<string> { signerKey });

    bundle.AddAutoplacement(
        documentKey: documentKey,
        kind: FieldKind.SigningDate,
        search: "[DATE_HERE]",
        w: 120,
        h: 24,
        offsetX: 0,
        offsetY: 0,
        editors: new List<string> { signerKey });

    var created = client.BundleResource
        .CreateBundleUploadFilesFromHelper(bundle)
        .Execute();

    Console.WriteLine($"Created bundle: {created.Id}");
}
```

If you already manage reusable templates in Blueink, the SDK also supports:

- `BundleResource.CreateBundleFromEnvelopeTemplate(...)`
- `BundleHelper.AddDocumentTemplate(...)`

### Create an embedded signing URL

```csharp
using System;
using Blueink.Client.Net.v2;

using (var client = new BlueinkService())
{
    var signing = client.PacketResource
        .CreateEmbeddedSigningUrl("your-packet-id")
        .Execute();

    Console.WriteLine(signing.Url);
    Console.WriteLine(signing.Expires);
}
```

### Check your rate limit status

```csharp
using System;
using Blueink.Client.Net.v2;

using (var client = new BlueinkService())
{
    var rateLimit = client.RateLimitResource
        .CheckRateLimitStatus()
        .Execute();

    Console.WriteLine($"Remaining: {rateLimit.Remaining}/{rateLimit.Limit}");
}
```

## Available Resources

| Resource | Common operations |
| --- | --- |
| `BundleResource` | List, retrieve, create, cancel, expire, list events, list files, list data, add tags, remove tags, create from envelope templates |
| `PacketResource` | Retrieve, update, send reminder, create embedded signing URL, retrieve certificate of evidence |
| `TemplateResource` | List and retrieve document templates and envelope templates |
| `PersonResource` | List, retrieve, create, update, partially update, delete |
| `WebhookResource` | List, retrieve, create, update, partially update, delete, manage headers, inspect deliveries and events, manage webhook secrets |
| `RateLimitResource` | Check current API rate limit status |

## Error Handling

The SDK throws configuration and API exceptions with structured context:

- `BlueinkConfigurationException`: missing or invalid client configuration
- `BlueinkApiException`: API request failures with Blueink error details when available
- `BlueinkAuthenticationException`: authentication failures
- `BlueinkValidationException`: client-side validation failures
- `BlueinkNotFoundException`: missing resources
- `BlueinkRateLimitException`: rate limiting

```csharp
using System;
using Blueink.Client.Net.v2;

try
{
    using (var client = new BlueinkService())
    {
        var bundle = client.BundleResource.GetBundle("bundle-id").Execute();
        Console.WriteLine(bundle.Label);
    }
}
catch (BlueinkConfigurationException ex)
{
    Console.WriteLine($"Configuration error: {ex.Message}");
}
catch (BlueinkApiException ex)
{
    Console.WriteLine($"Blueink API error: {ex.Message}");
}
```

## Development

Run the unit tests:

```bash
dotnet test Tests/Blueink.Client.Net.v2.Tests.csproj /p:GeneratePackageOnBuild=false
```

Build the library without packing:

```bash
dotnet build Blueink.Client.Net.v2.csproj /p:GeneratePackageOnBuild=false
```

This project is configured to include `README.md` and `LICENSE` in the NuGet package. If you intend to pack or publish the library, make sure both files exist at the repository root.

## Documentation

- [Blueink API reference](https://developer.blueink.com/docs/api/)
- [Blueink authentication guide](https://developer.blueink.com/docs/esignature-api/authentication/)
- [Blueink quick start](https://developer.blueink.com/docs/quick-start/)

## Repository

- Source: [blueinkhq/blueink-client-dotnet](https://github.com/blueinkhq/blueink-client-dotnet)
