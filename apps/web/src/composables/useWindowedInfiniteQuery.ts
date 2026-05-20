import { computed } from 'vue'
import {
  useInfiniteQuery,
  useQueryClient,
  type QueryKey,
} from '@tanstack/vue-query'

export type Page<T> = {
  items: T[]
  nextCursor: string | null
  prevCursor: string | null
}

type Options<T> = {
  queryKey: QueryKey
  queryFn: (cursor: string | null) => Promise<Page<T>>
  maxPages?: number // NEW (important)
}

export function useWindowedInfiniteQuery<T>(
  options: Options<T>,
) {
  const queryClient = useQueryClient()

  const maxPages = options.maxPages ?? 10

  const query = useInfiniteQuery({
    queryKey: options.queryKey,

    initialPageParam: null,

    queryFn: ({ pageParam }: { pageParam: string | null }) =>
      options.queryFn(pageParam),

    getNextPageParam: lastPage => lastPage.nextCursor,
    getPreviousPageParam: firstPage => firstPage.prevCursor,
  })

  /**
   * Trim oldest pages to keep memory bounded while preserving query state
   */
  function evictPages() {
    const data = query.data.value
    if (!data) return

    const { pages, pageParams } = data

    if (pages.length <= maxPages) return

    // Keep only the most recent maxPages pages (trim from the start)
    const keepFrom = pages.length - maxPages
    const trimmedPages = pages.slice(keepFrom)
    const trimmedPageParams = pageParams.slice(keepFrom)

    // Update the query cache without destroying the query state
    queryClient.setQueryData(options.queryKey, (oldData: any) => ({
      ...oldData,
      pages: trimmedPages,
      pageParams: trimmedPageParams,
    }))
  }

  async function fetchNextPage() {
    await query.fetchNextPage()
    // Don't evict immediately - let user scroll first
  }

  async function fetchPreviousPage() {
    await query.fetchPreviousPage()
    // Don't evict immediately - let user scroll first
  }

  /**
   * UI flattened items (virtualized)
   */
const items = computed<T[]>(() =>
  query.data.value?.pages.flatMap(p => p.items) ?? [],
)

  return {
    data: items,
    isFetching: query.isFetching,
    isLoading: query.isLoading,
    hasNextPage: query.hasNextPage,
    fetchNextPage,
    fetchPreviousPage,
  }
}