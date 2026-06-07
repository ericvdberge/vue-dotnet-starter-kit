-- Enable pgvector extension
CREATE EXTENSION IF NOT EXISTS vector;

CREATE TABLE users (
    id UUID PRIMARY KEY DEFAULT uuidv7(),
    name VARCHAR(255) NOT NULL,
    email VARCHAR(255) UNIQUE NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    active BOOLEAN DEFAULT TRUE
);

CREATE TABLE roles (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    name TEXT NOT NULL UNIQUE
);

INSERT INTO users (
    id,
    name,
    email,
    created_at,
    active
)
SELECT
    uuidv7(),
    'User ' || gs,
    'user' || gs || '@example.com',
    NOW() - (random() * INTERVAL '365 days'),
    random() > 0.1
FROM generate_series(1, 6000) AS gs;

INSERT INTO roles (name)
VALUES 
    ('Admin'),
    ('Editor'),
    ('Viewer'),
    ('Manager'),
    ('Guest');