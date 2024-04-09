using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Logic
{
    static class TemplatesLogic
    {
        internal static AliquotTemplate GetAliquotTemplateByWorkfloeNode(Entities context, WorkflowNode workflowNode)
        {
            return
                (from item in context.AliquotTemplates.Where(x => x.AliquotTemplateId == workflowNode.Template)
                 select item).FirstOrDefault();
        }
    }
}
