import { useQuery } from "@tanstack/vue-query";
import type { User } from "../types/users"
import users from './users.json';

export const useUsers = () => {
    const usersQuery = useQuery({
        queryKey: ['users'],
        queryFn: async () => {
            return await Promise.resolve(users as User[]);
        }
    })
    
    return { usersQuery };
}