import { 
    getCoreRowModel, 
    getFilteredRowModel,
    useVueTable 
} from "@tanstack/vue-table"
import type { Role } from "../types/roles"
import { computed, unref, type Ref } from "vue"

export const useRolesTable = (roles: Ref<Role[] | undefined>) => {
    const columns = computed(() => {
        if (!roles.value?.length) return [];
        return Object.keys(roles.value[0]!).map(key => ({
            accessorKey: key,
            header: key.charAt(0).toUpperCase() + key.slice(1)
        }))
    })

    const table = useVueTable({
        get data() {
            return unref(roles) ?? []
        },
        get columns() {
            return unref(columns);
        },
        getCoreRowModel: getCoreRowModel(),
        getFilteredRowModel: getFilteredRowModel(), // 🔥 REQUIRED
        globalFilterFn: 'includesString', // simple contains filter
    })

    return {
        table
    }
}