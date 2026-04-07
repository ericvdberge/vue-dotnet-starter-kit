import { defineConfig } from '@hey-api/openapi-ts';
import "dotenv/config";

export default defineConfig({
  input: `${process.env.VITE_API_BASE_URL}/openapi/v1.json`,
  output: 'src/generated',

  plugins: [
    '@tanstack/vue-query',
  ],
});