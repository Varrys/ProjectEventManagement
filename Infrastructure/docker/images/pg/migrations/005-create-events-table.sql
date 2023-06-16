CREATE TABLE events
(
    eventId     uuid PRIMARY KEY DEFAULT uuid_generate_v4(),
    name        VARCHAR(255) NOT NULL,
    date        TIMESTAMP        DEFAULT NOW(),
    location    VARCHAR(255) NOT NULL,
    description VARCHAR(255) NOT NULL,
    maxCapacity INTEGER      NOT NULL,
    userId      uuid REFERENCES users (userId)
);