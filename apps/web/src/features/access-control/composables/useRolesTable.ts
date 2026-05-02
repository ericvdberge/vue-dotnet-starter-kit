import { 
    getCoreRowModel, 
    getFilteredRowModel,
    getPaginationRowModel, 
    getSortedRowModel, 
    useVueTable 
} from "@tanstack/vue-table"
import type { Role } from "../types/roles"

export const useRolesTable = () => {
    const data: Role[] = [
        { id: 1, name: 'Admin', description: 'Administrator role with full permissions' },
        { id: 2, name: 'Editor', description: 'Editor role with permissions to edit content' },
        { id: 3, name: 'Viewer', description: 'Viewer role with read-only permissions' },
    ]

    const columns = Object.keys(data[0]!).map(key => ({
        accessorKey: key,
        header: key.charAt(0).toUpperCase() + key.slice(1)
    }))

    const table = useVueTable({
        data,
        columns,
        getCoreRowModel: getCoreRowModel(),
        getSortedRowModel: getSortedRowModel(),
        getFilteredRowModel: getFilteredRowModel(),
        getPaginationRowModel: getPaginationRowModel(),
    })

    return {
        table
    }
}