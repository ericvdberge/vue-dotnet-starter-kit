import type { User } from "../types/users"
import users from './users.json';
import { useWindowedInfiniteQuery, type Page } from "@/composables/useWindowedInfiniteQuery";

const PAGE_SIZE = 50

const fetchUsers = async (
  cursor: string | null,
): Promise<Page<User>> => {
    console.log('fetchUsers: cursor=', cursor);

    await new Promise(resolve => setTimeout(resolve, 500)); // Simulate network delay

  const start = cursor ? Number(cursor) : 1


  const items: User[] = users.sort((a, b) => a.id - b.id).slice(start, start + PAGE_SIZE) as User[];

  return Promise.resolve({
    items,
    nextCursor:
      start + PAGE_SIZE < users.length
        ? String(start + PAGE_SIZE)
        : null,

    prevCursor:
      start - PAGE_SIZE >= 0
        ? String(start - PAGE_SIZE)
        : null,
  })
}


export const useUsers = () => {
    // const usersQuery = useQuery({
    //     queryKey: ['users'],
    //     queryFn: async () => {
    //         return await Promise.resolve(users);
    //     }
    // })

    const getUsers = () => {
        return useWindowedInfiniteQuery({
            queryKey: ['users'],
            maxPages: 3,
            queryFn: cursor => fetchUsers(cursor),
        })
    }
    
    return { getUsers };
}