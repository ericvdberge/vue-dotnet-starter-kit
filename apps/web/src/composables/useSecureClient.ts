import { useAuth } from "@/composables/useAuth";
import { client } from "@/generated/client.gen";

export const useSecureClient = (): void => {
    //set baseurl for client
    client.setConfig({
        baseUrl: import.meta.env.VITE_API_BASE_URL,
    })
    
    // Add an interceptor to the client to add the authorization token to requests
    client.interceptors.request.use((config) => {
        // Middleware is great for adding authorization tokens to requests made to
        // protected paths. Headers are set randomly here to allow surfacing the
        // default headers, too.
        const { getToken } = useAuth();
        const token = getToken();
        config.headers.set('Authorization', `Bearer ${token}`);
        return config;
    });
}