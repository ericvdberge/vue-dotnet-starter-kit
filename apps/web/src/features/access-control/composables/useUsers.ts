import { useInfiniteQuery } from "@tanstack/vue-query";
import type { User } from "../types/users"
import users from './users.json';
import type { Page } from "../types/shared";
const PAGE_SIZE = 100;

const fetchUsers = async (
  cursor: string | null,
): Promise<Page<User>> => {
  await new Promise(resolve => setTimeout(resolve, 500)); // Simulate network delay
  const start = cursor ? Number(cursor) : 0
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
    const getUsers = () => {
        return useInfiniteQuery({
          queryKey: ['users'],
          initialPageParam: null,
          queryFn: ({ pageParam }: { pageParam: string | null }) => fetchUsers(pageParam),
          getNextPageParam: lastPage => lastPage.nextCursor,
          getPreviousPageParam: firstPage => firstPage.prevCursor,
        })
    }
    
    return { getUsers };
}