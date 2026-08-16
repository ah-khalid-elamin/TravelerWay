# TravelerWay POC Backend

## Solution layout

- TravelerWay.Api
  - Controllers for bookings and Stripe webhooks
  - Data/DbContext for PostgreSQL persistence
  - Extensions/ service registration
- TravelerWay.Common
  - Shared domain models and request/response payloads
- TravelerWay.Services
  - Booking policy logic
  - Duffel integration placeholder
  - Stripe checkout session creation
- TravelerWay.Services.Tests
  - Business rule tests for pricing, cancellations, and rescheduling

## n8n + Telegram flow

1. n8n receives a Telegram message from the user.
2. n8n calls the TravelerWay booking API endpoints.
3. The API orchestrates the flow using the policy service, Duffel service, and Stripe service.
4. Stripe redirects the user to a checkout page and posts checkout events back to the webhook endpoint.

## Suggested webhook endpoints

- POST /api/bookings/search
- POST /api/bookings/checkout
- POST /api/bookings/book
- POST /api/webhooks/stripe
